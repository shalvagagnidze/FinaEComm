using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Models.FinaModels;
using ServiceLayer.Services.FinaServices.FinaHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Services.FinaServices
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly FinaApiClient _apiClient;

        public ProductGroupService(FinaApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<FinaProductGroupModel>> GetProductGroupsAsync()
        {
            var response = await _apiClient.GetAsync<GroupsApiResponse<List<FinaProductGroupModel>>>("getProductGroups");
            return response?.Groups ?? new List<FinaProductGroupModel>();
        }

        public async Task<IEnumerable<FinaProductGroupModel>> GetWebProductGroupsAsync()
        {
            var response = await _apiClient.GetAsync<GroupsApiResponse<List<FinaProductGroupModel>>>("getWebProductGroups");
            return response?.Groups ?? new List<FinaProductGroupModel>();
        }

        public async Task<FinaProductGroupModel> GetProductGroupByIdAsync(int id)
        {
            var groups = await GetProductGroupsAsync();
            return groups.FirstOrDefault(g => g.Id == id)!;
        }

        public async Task<IEnumerable<FinaProductGroupModel>> GetChildGroupsAsync(int parentId)
        {
            var groups = await GetProductGroupsAsync();
            return groups.Where(g => g.ParentId == parentId);
        }
    }

    public class GroupsApiResponse<T>
    {
        [JsonPropertyName("groups")]
        public T? Groups { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
