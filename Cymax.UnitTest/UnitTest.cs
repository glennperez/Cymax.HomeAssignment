using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Cymax.Console.Client.Models;
using Cymax.Console.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cymax.UnitTest;

public class UnitTest
{
    private readonly Request _request = new()
    {
        SourceAddress = "3909 Witmer Rd, Niagara Falls, NY 14305, United States",
        DestinationAddress = "5010 Indian River Dr, Las Vegas, NV 89103, United States",
        CartonDimensions = new[] { 14, 25, 37 }
    };
    
    [Fact]
    public async void API_Company_1_Is_Listening()
    {
        var inputJson = new StringContent(JsonSerializer.Serialize(new
            {
                contactAddress = "input.SourceAddress",
                warehouseAddress = "input.DestinationAddress",
                packageDimensions = new[] {1, 2, 3}
            }),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        
        using var client = new HttpClient();
        var result = await client.PostAsync("http://localhost:5161/deals", inputJson);
        Assert.True(result.IsSuccessStatusCode);
    }
    
    [Fact]
    public async void API_Company_2_Is_Listening()
    {
        var inputJson = new StringContent(JsonSerializer.Serialize(new
            {
                consignee = "input.SourceAddress",
                consignor = "input.DestinationAddress",
                cartons = new[] {1, 2, 3}
            }),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        
        using var client = new HttpClient();
        var result = await client.PostAsync("http://localhost:5039/offers", inputJson);
        Assert.True(result.IsSuccessStatusCode);
    }
    
    [Fact]
    public async void API_Company_3_Is_Listening()
    {
        StringBuilder request = new($"<?xml version='1.0' encoding='UTF-8'?>\n" +
                                    $"<root>\n<source>source</source>\n" +
                                    $"<destination>destination</destination>\n" +
                                    $"<packages><package>5</package></packages>\n</root>");
        
        var inputXml = new StringContent(request.ToString(),
            Encoding.UTF8,
            MediaTypeNames.Application.Xml);
        
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Accept", "application/xml");
        var result = await client.PostAsync("http://localhost:5000/bids", inputXml);
        Assert.True(result.IsSuccessStatusCode);
    }
    
    [Fact]
    public async void Service_Company1_Is_Ready_For_Use()
    {
        var host = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddTransient<Company1Service>();
            })
            .Build();
    
        var company1Service = host.Services.GetRequiredService<Company1Service>();
        var postDealsCompany1Task = await company1Service.PostDeal(_request);
        Assert.NotNull(postDealsCompany1Task);
    }
    
    [Fact]
    public async void Service_Company2_Is_Ready_For_Use()
    {
        var host = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddTransient<Company2Service>();
            })
            .Build();
    
        var company1Service = host.Services.GetRequiredService<Company2Service>();
        var postDealsCompany1Task = await company1Service.PostDeal(_request);
        Assert.NotNull(postDealsCompany1Task);
    }
    
    [Fact]
    public async void Service_Company3_Is_Ready_For_Use()
    {
        var host = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddTransient<Company3Service>();
            })
            .Build();
    
        var company1Service = host.Services.GetRequiredService<Company3Service>();
        var postDealsCompany1Task = await company1Service.PostDeal(_request);
        Assert.NotNull(postDealsCompany1Task);
    }

    [Fact]
    public async void Create_Orchestrator_And_Call_Services()
    {
        var host = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddTransient<Company1Service>();
                services.AddTransient<Company2Service>();
                services.AddTransient<Company3Service>();
            })
            .Build();

        var orchestrator = new OrchestratorService(host);
        var result = await orchestrator.GetBestDealsFromSuppliersParallel(_request);
        
        Assert.True(result.BestDeal.Value> 0);
    }
    
}
