using Cymax.Console.Client.Services;

namespace Cymax.Console.Client.Models
{
    /// <summary>
    /// Class utility for merge All response and manage his presentation in the console App.
    /// </summary>
    public class Response
    {
        private ResponseCompany1? Company1 { get; set; }
        private ResponseCompany2? Company2 { get; set; }
        private ResponseCompany3? Company3 { get; set; }

        private readonly List<ICompanyResponse> _companies;

        public KeyValuePair<string, int> BestDeal
        {
            get
            {
                var supplier = _companies.MinBy(c => c.Deal);
                return supplier is not null ? new KeyValuePair<string, int>(supplier.CompanyName, supplier.Deal) : new KeyValuePair<string, int>("", 0);
            }
        }

        public Response(ResponseCompany1? company1,
                            ResponseCompany2? company2,
                                ResponseCompany3? company3)
        {
            Company1 = company1;
            Company2 = company2;
            Company3 = company3;
            _companies = new List<ICompanyResponse>();
            
            if(company1 is not null)
                _companies.Add(company1);
            if(company2 is not null)
                _companies.Add(company2);
            if(company3 is not null)
                _companies.Add(company3);
        }

        public Dictionary<string, string?> PrintAllSuppliers()
        {
            return new Dictionary<string, string?>
            {
                {"Company1", Company1 is not null ? Company1?.Deal.ToString() : "Offline"},
                {"Company2", Company2 is not null ? Company2?.Deal.ToString() : "Offline"},
                {"Company3", Company3 is not null ? Company3?.Deal.ToString() : "Offline"}
            };
        }
    }
}

