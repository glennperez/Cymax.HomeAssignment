﻿using System;
using Cymax.Console.Client.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cymax.Console.Client.Services
{
    public class OrchestratorService
    {
        public readonly Company1Service _company1Service;
        public readonly Company2Service _company2Service;
        public readonly Company3Service _company3Service;

        public OrchestratorService(IHost host)
        {
            _company1Service = host.Services.GetRequiredService<Company1Service>();
            _company2Service = host.Services.GetRequiredService<Company2Service>();
            _company3Service = host.Services.GetRequiredService<Company3Service>();
        }

        public async Task<Response> GetBestDealsFromSuplyersParallel(Request data)
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
