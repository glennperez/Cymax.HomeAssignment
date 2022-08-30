using Cymax.Console.Client.Models;
using Cymax.Console.Client.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var request = new Request()
        {
            SourceAddress = "3909 Witmer Rd, Niagara Falls, NY 14305, United States",
            DestinationAddress = "5010 Indian River Dr, Las Vegas, NV 89103, United States",
            CartonDimensions = new int[3] { 14, 25, 37 }
        };

        var orchestrator = new OrchestratorService();
        var result = await orchestrator.GetBestDealsFromSuppliersParallel(request);

        Console.WriteLine(string.IsNullOrEmpty(result.BestDeal.Key)
            ? "Best deal is not available at this time.\n"
            : $"Best deal is: {result.BestDeal.Value} and is from: {result.BestDeal.Key}\n");

        Console.WriteLine("Consulted Suppliers:");
        foreach (var item in result.PrintAllSuppliers())
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}