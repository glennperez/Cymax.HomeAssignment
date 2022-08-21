using Cymax.Console.Client.Models;
using Cymax.Console.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

        try
        {
            var company1Service = host.Services.GetRequiredService<Company1Service>();
            var company2Service = host.Services.GetRequiredService<Company2Service>();
            var company3Service = host.Services.GetRequiredService<Company2Service>();


            var request = new InputData() {
                                SourceAddress = "3909 Witmer Rd, Niagara Falls, NY 14305, United States",
                                DestinationAddress = "5010 Indian River Dr, Las Vegas, NV 89103, United States",
                                CartonDimensions = new int[3] { 14, 25, 37 }
                            };

            var res1 = await company1Service.PostDeals(request);
            var res2 = await company2Service.PostDeals(request);
            var res3 = await company3Service.PostDeals(request);

            Console.WriteLine(res1.Deal);
            Console.WriteLine(res2.Deal);
            Console.WriteLine(res3.Deal);
        }
        catch (Exception ex)
        {
            host.Services.GetRequiredService<ILogger<Program>>()
                            .LogError(ex, "No se puede cargar desde algunas empresas.");
        }
    }
}