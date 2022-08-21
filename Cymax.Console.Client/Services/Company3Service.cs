using System;
using Cymax.Console.Client.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cymax.Console.Client.Services
{
    public class Company3Service
    {
        private readonly HttpClient _httpClient;

        public Company3Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/bids");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
        }

        public async Task<ResponseCompany3> PostDeals(InputData input)
        {
            StringBuilder request = new($"<?xml version='1.0' encoding='UTF-8'?>\n" +
                                        $"<root>\n<source>{input.SourceAddress}</source>\n" +
                                        $"<destination>{input.DestinationAddress}</destination>\n" +
                                        $"<packages>\n");

            foreach (int pkg  in input.CartonDimensions)
            {
                request.Append($"<package>{pkg}</package>\n");
            }

            request.Append("</packages>\n</root>");

            var inputXml = new StringContent(request.ToString(),
                                                Encoding.UTF8,
                                                Application.Xml);

            using var httpResponseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress, inputXml);

            httpResponseMessage.EnsureSuccessStatusCode();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var serializer = new XmlSerializer(typeof(ResponseCompany3));
                var response = await httpResponseMessage.Content.ReadAsStreamAsync();
                return serializer.Deserialize(response) as ResponseCompany3;
            }

            return null;
        }
    }

    public class ResponseCompany3
    {
        [XmlElement(ElementName = "quote")]
        public int Deal { get; set; }
    }
}

