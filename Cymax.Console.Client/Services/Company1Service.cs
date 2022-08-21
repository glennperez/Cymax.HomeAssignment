﻿using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cymax.Console.Client.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cymax.Console.Client.Services
{
    public class Company1Service
    {
        private readonly HttpClient _httpClient;

        public Company1Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5161/deals");
        }

        public async Task<ResponseCompany1> PostDeals(InputData input)
        {
            var inputJson = new StringContent(JsonSerializer.Serialize( new {
                                                        contactAddress = input.SourceAddress,
                                                        warehouseAddress = input.DestinationAddress,
                                                        packageDimensions = input.CartonDimensions
                                                    }),
                                                Encoding.UTF8,
                                                Application.Json);

            using var httpResponseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress, inputJson);

            httpResponseMessage.EnsureSuccessStatusCode();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseJsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ResponseCompany1>(responseJsonString);
            }

            return null;
        }
    }

    public class ResponseCompany1
    {
        [JsonPropertyName("total")]
        public int Deal { get; set; }
    }
}
