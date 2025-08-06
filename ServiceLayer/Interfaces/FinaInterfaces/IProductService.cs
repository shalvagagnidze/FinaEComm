using DomainLayer.Entities.Products;
using ServiceLayer.Models.FinaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.FinaInterfaces
{
    public interface IProductService
    {
        Task<IEnumerable<FinaProductModel>> GetProductsAsync();
        Task<IEnumerable<FinaProductPriceModel>> GetProductPricesAsync();
        //Task<IEnumerable<ProductUnit>> GetProductUnitsAsync();
        //Task<IEnumerable<Product>> GetProductsByIdsAsync(int[] productIds);
        //Task<IEnumerable<object>> GetProductAdditionalFieldsAsync();
        //Task<IEnumerable<object>> GetProductImagesAsync(int productId);
        //Task<IEnumerable<object>> GetProductsImageArrayAsync(int[] productIds);
        //Task<IEnumerable<object>> GetProductsRestAsync();
    }
}
