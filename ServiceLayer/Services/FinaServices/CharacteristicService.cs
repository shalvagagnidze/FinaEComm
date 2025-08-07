using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Models.FinaModels.Facets;
using ServiceLayer.Services.FinaServices.FinaHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Services.FinaServices
{
    public class CharacteristicService : ICharacteristicService
    {
        private readonly FinaApiClient _apiClient;

        public CharacteristicService(FinaApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<FinaCharacteristicModel>> GetCharacteristicsAsync()
        {
            var response = await _apiClient.GetAsync<ApiResponse<List<FinaCharacteristicModel>>>("getCharacteristics");
            return response?.Characteristics ?? new List<FinaCharacteristicModel>();
        }

        public async Task<IEnumerable<FinaProductCharacteristicModel>> GetCharacteristicValuesAsync()
        {
            var response = await _apiClient.GetAsync<ApiResponse<List<FinaProductCharacteristicModel>>>("getCharacteristicValues");
            return response?.CharacteristicValues ?? new List<FinaProductCharacteristicModel>();
        }
    }

    public class ApiResponse<T>
    {
        [JsonPropertyName("characteristics")]
        public T? Characteristics { get; set; }

        [JsonPropertyName("characteristic_values")]
        public T? CharacteristicValues { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
