using Cymax.Console.Client.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cymax.Console.Client.Services
{
    /// <summary>
    /// Orchestrator class that perform in Parallel al APIs Calls for better performance management.
    /// </summary>
    public class OrchestratorService
    {
        private readonly Company1Service _company1Service;
        private readonly Company2Service _company2Service;
        private readonly Company3Service _company3Service;

        public OrchestratorService(IHost host)
        {
            _company1Service = host.Services.GetRequiredService<Company1Service>();
            _company2Service = host.Services.GetRequiredService<Company2Service>();
            _company3Service = host.Services.GetRequiredService<Company3Service>();
        }

        public async Task<Response> GetBestDealsFromSuppliersParallel(Request data)
        {
            var postDealsCompany1Task = _company1Service.PostDeal(data);
            var postDealsCompany2Task = _company2Service.PostDeal(data);
            var postDealsCompany3Task = _company3Service.PostDeal(data);

            await Task.WhenAll(postDealsCompany1Task, postDealsCompany2Task, postDealsCompany3Task);

            var postDealsCompany1 = await postDealsCompany1Task;
            var postDealsCompany2 = await postDealsCompany2Task;
            var postDealsCompany3 = await postDealsCompany3Task;

            return new Response(postDealsCompany1, postDealsCompany2, postDealsCompany3);
        }
    }
}

