using System;
using Cymax.Console.Client.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace Cymax.Console.Client.Services
{
    public class Company2Service : IService<ResponseCompany2>
    {
        private readonly HttpClient _httpClient;

        public Company2Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5039/offers");
        }

        public async Task<ResponseCompany2> PostDeal(Request input)
        {
            var inputJson = new StringContent(JsonSerializer.Serialize(new
            {
                consignee = input.SourceAddress,
                consignor = input.DestinationAddress,
                cartons = input.CartonDimensions
            }),
                                                Encoding.UTF8,
                                                Application.Json);

            try
            {
                using var httpResponseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress, inputJson);

                httpResponseMessage.EnsureSuccessStatusCode();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var responseJsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ResponseCompany2>(responseJsonString);
                }
            }
            catch (Exception ex)
            {
                Debug.Write($"Could not connect to company 2 - Reasone:{ex.Message}");
            }

            return null;
        }
    }

    public class ResponseCompany2
    {
        [JsonPropertyName("amount")]
        public int Deal { get; set; }
        public string CompanyName { get; set; } = "Company2";
    }

}

