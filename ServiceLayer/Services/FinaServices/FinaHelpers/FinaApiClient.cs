using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Services.FinaServices.FinaHelpers
{
    public class FinaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly TokenStorageService _tokenStorage;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public FinaApiClient(HttpClient httpClient, IConfiguration configuration, TokenStorageService tokenStorage)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["FinaApi:BaseUrl"]!;
            _tokenStorage = tokenStorage;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetAuthToken(string token)
        {
            _tokenStorage.SetToken(token);
            UpdateAuthorizationHeader();
        }

        private void UpdateAuthorizationHeader()
        {
            var token = _tokenStorage.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            UpdateAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/operation/{endpoint}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(content, _jsonSerializerOptions)
                   ?? throw new InvalidOperationException("Deserialization returned null.");
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            UpdateAuthorizationHeader();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/operation/{endpoint}", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions)
                   ?? throw new InvalidOperationException("Deserialization returned null.");
        }
    }
}
