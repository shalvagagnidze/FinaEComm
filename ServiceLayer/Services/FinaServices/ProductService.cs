using DomainLayer.Entities.Products;
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
    public class ProductService : IProductService
    {
        private readonly FinaApiClient _apiClient;

        public ProductService(FinaApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<FinaProductModel>> GetProductsAsync()
        {
            var response = await _apiClient.GetAsync<ProductsApiResponse<List<FinaProductModel>>>("getProducts");
            return response?.Products ?? new List<FinaProductModel>();
        }

        public async Task<IEnumerable<FinaProductPriceModel>> GetProductPricesAsync()
        {
            var response = await _apiClient.GetAsync<ProductsApiResponse<List<FinaProductPriceModel>>>("getProductPrices");
            return response?.Prices ?? new List<FinaProductPriceModel>();
        }

        //public async Task<IEnumerable<ProductUnit>> GetProductUnitsAsync()
        //{
        //    var response = await _apiClient.GetAsync<ProductsApiResponse<List<ProductUnit>>>("getProductUnits");
        //    return response?.units ?? new List<ProductUnit>();
        //}

        //public async Task<IEnumerable<Product>> GetProductsByIdsAsync(int[] productIds)
        //{
        //    var response = await _apiClient.PostAsync<ProductsApiResponse<List<Product>>>("getProductsArray", productIds);
        //    return response?.products ?? new List<Product>();
        //}

        //public async Task<IEnumerable<object>> GetProductAdditionalFieldsAsync()
        //{
        //    var response = await _apiClient.GetAsync<ProductsApiResponse<List<object>>>("getProductAdditionalFields");
        //    return response?.fields ?? new List<object>();
        //}

        //public async Task<IEnumerable<object>> GetProductImagesAsync(int productId)
        //{
        //    var response = await _apiClient.GetAsync<ProductsApiResponse<List<object>>>($"getProductImages/{productId}");
        //    return response?.images ?? new List<object>();
        //}

        //public async Task<IEnumerable<object>> GetProductsImageArrayAsync(int[] productIds)
        //{
        //    var response = await _apiClient.PostAsync<ProductsApiResponse<List<object>>>("getProductsImageArray", productIds);
        //    return response?.images ?? new List<object>();
        //}

        //public async Task<IEnumerable<object>> GetProductsRestAsync()
        //{
        //    var response = await _apiClient.GetAsync<ProductsApiResponse<List<object>>>("getProductsRest");
        //    return response?.products ?? new List<object>();
        //}
    }

    public class ProductsApiResponse<T>
    {
        [JsonPropertyName("products")]
        public T? Products { get; set; }

        [JsonPropertyName("prices")]
        public T? Prices { get; set; }

        [JsonPropertyName("units")]
        public T? Units { get; set; }

        [JsonPropertyName("fields")]
        public T? Fields { get; set; }

        [JsonPropertyName("images")]
        public T? Images { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
