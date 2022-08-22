using System;
using Cymax.Console.Client.Services;

namespace Cymax.Console.Client.Models
{
    public class Response
    {
        private ResponseCompany1 Company1 { get; set; }
        private ResponseCompany2 Company2 { get; set; }
        private ResponseCompany3 Company3 { get; set; }

        public Response(ResponseCompany1 company1, ResponseCompany2 company2, ResponseCompany3 company3)
        {
            Company1 = company1;
            Company2 = company2;
            Company3 = company3;
        }

        public int GetBestSeller()
        {
            return Math.Min(Company1.Deal, Math.Min(Company2.Deal, Company3.Deal));
        }
    }
}

