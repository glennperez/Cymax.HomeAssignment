using Cymax.Console.Client.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Cymax.Console.Client.Services
{
    /// <summary>
    /// Base service with the own consuming implementation for de Typed Class.
    /// </summary>
    public class Company3Service : IService<ResponseCompany3>
    {
        private readonly HttpClient _httpClient;

        public Company3Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/bids");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
        }

        public async Task<ResponseCompany3?> PostDeal(Request input)
        {
            StringBuilder request = new($"<?xml version='1.0' encoding='UTF-8'?>\n" +
                                         $"<root>\n<source>{input.SourceAddress}</source>\n" +
                                         $"<destination>{input.DestinationAddress}</destination>\n" +
                                         $"<packages>\n");

            foreach (int pkg in input.CartonDimensions)
            {
                request.Append($"<package>{pkg}</package>\n");
            }

            request.Append("</packages>\n</root>");

            var inputXml = new StringContent(request.ToString(),
                                                Encoding.UTF8,
                                                Application.Xml);

            try
            {
                using var httpResponseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress, inputXml);

                httpResponseMessage.EnsureSuccessStatusCode();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var serializer = new XmlSerializer(typeof(ResponseCompany3));
                    var response = await httpResponseMessage.Content.ReadAsStreamAsync();
                    return serializer.Deserialize(response) as ResponseCompany3;
                }
            }
            catch (Exception ex)
            {
                Debug.Write($"Could not connect to company 3 - Reasone:{ex.Message}");
            }

            return null;
        }
    }

    [XmlRoot(ElementName = "xml")]
    public class ResponseCompany3 : ICompanyResponse
    {
        [XmlElement(ElementName = "quote")]
        public int Deal { get; set; }
        public string CompanyName { get; set; } = "Company3";
    }
}

