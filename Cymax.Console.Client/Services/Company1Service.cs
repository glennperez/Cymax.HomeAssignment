using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cymax.Console.Client.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cymax.Console.Client.Services
{
    public class Company1Service : IService<ResponseCompany1>
    {
        private readonly HttpClient _httpClient;

        public Company1Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5161/deals");
        }

        public async Task<ResponseCompany1> PostDeal(Request input)
        {
            var inputJson = new StringContent(JsonSerializer.Serialize(new
            {
                contactAddress = input.SourceAddress,
                warehouseAddress = input.DestinationAddress,
                packageDimensions = input.CartonDimensions
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
                    return JsonSerializer.Deserialize<ResponseCompany1>(responseJsonString);
                }
            }
            catch (Exception ex)
            {
                Debug.Write($"Could not connect to company 1 - Reasone:{ex.Message}");
            }

            return null;
        }
    }

    public class ResponseCompany1
    {
        [JsonPropertyName("total")]
        public int Deal { get; set; }
        public string CompanyName { get; set; } = "Company1";
    }
}

