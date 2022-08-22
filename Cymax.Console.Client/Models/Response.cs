using System;
using Cymax.Console.Client.Services;

namespace Cymax.Console.Client.Models
{
    public class Response
    {
        private ResponseCompany1 Company1 { get; set; }
        private ResponseCompany2 Company2 { get; set; }
        private ResponseCompany3 Company3 { get; set; }

        public KeyValuePair<string, int> BestDeal => (
                    Company1.Deal < Company2.Deal ?
                    (Company1.Deal < Company3.Deal ?
                            new Dictionary<string, int>() { { Company1.CompanyName, Company1.Deal } } :
                            new Dictionary<string, int>() { { Company3.CompanyName, Company3.Deal } })
                            :
                    (Company2.Deal < Company3.Deal ?
                            new Dictionary<string, int>() { { Company2.CompanyName, Company2.Deal } } :
                            new Dictionary<string, int>() { { Company3.CompanyName, Company3.Deal } })
            ).FirstOrDefault();

        public Response(ResponseCompany1 company1,
                            ResponseCompany2 company2,
                                ResponseCompany3 company3)
        {
            Company1 = company1;
            Company2 = company2;
            Company3 = company3;
        }

        public Dictionary<string,int> PrintAllSuplyers()
        {
            return new Dictionary<string, int>()
            {
                { Company1.CompanyName, Company1.Deal },
                { Company2.CompanyName, Company2.Deal },
                { Company3.CompanyName, Company3.Deal },
            };
        }
    }
}

