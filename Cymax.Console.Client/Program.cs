using Cymax.Console.Client.Models;
using Cymax.Console.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cymax.Console.Client;

internal class Program
{
    private static async Task Main(string[] args)
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

        var request = new Request()
        {
            SourceAddress = "3909 Witmer Rd, Niagara Falls, NY 14305, United States",
            DestinationAddress = "5010 Indian River Dr, Las Vegas, NV 89103, United States",
            CartonDimensions = new int[3] { 14, 25, 37 }
        };

        var orchestrator = new OrchestratorService(host);
        var result = await orchestrator.GetBestDealsFromSuppliersParallel(request);

        System.Console.WriteLine($"Best deal is: {result.BestDeal.Value} and is from: {result.BestDeal.Key}\n");
        System.Console.WriteLine("Consulted Suppliers:");
        foreach (var item in result.PrintAllSuppliers())
        {
            System.Console.WriteLine($"{item.Key}: {item.Value}");
        }
        
    }
}