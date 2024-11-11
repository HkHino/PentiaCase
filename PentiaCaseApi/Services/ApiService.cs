using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using PentiaCaseApi.Models;
using System.Collections.Generic;
using System;

namespace PentiaCaseApi.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;

            _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _apiSettings.ApiKey);
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }

        public async Task<List<Salesperson>> GetSalespeopleAsync()
        {
            return await GetAsync<List<Salesperson>>("SalesPersons");
        }

        public async Task<List<Orders>> GetOrdersAsync()
        {
            return await GetAsync<List<Orders>>("Orderlines");
        }
    }
}
