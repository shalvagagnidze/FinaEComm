using DomainLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Models.FinaModels;

namespace ECommerceOutdoor.Controllers.FinaControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinaProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public FinaProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                return Ok(new { products, ex = "" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { products = new List<FinaProductModel>(), ex = ex.Message });
            }
        }

        [HttpGet("prices")]
        public async Task<IActionResult> GetProductPrices()
        {
            try
            {
                var prices = await _productService.GetProductPricesAsync();
                return Ok(new { prices, ex = "" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { prices = new List<FinaProductPriceModel>(), ex = ex.Message });
            }
        }

        //[HttpGet("units")]
        //public async Task<IActionResult> GetProductUnits()
        //{
        //    try
        //    {
        //        var units = await _productService.GetProductUnitsAsync();
        //        return Ok(new { units, ex = (string)null });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok(new { units = new List<ProductUnit>(), ex = ex.Message });
        //    }
        //}

        //[HttpPost("array")]
        //public async Task<IActionResult> GetProductsByIds([FromBody] int[] productIds)
        //{
        //    try
        //    {
        //        var products = await _productService.GetProductsByIdsAsync(productIds);
        //        return Ok(new { products, ex = (string)null });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok(new { products = new List<Product>(), ex = ex.Message });
        //    }
        //}

        //[HttpGet("additional-fields")]
        //public async Task<IActionResult> GetProductAdditionalFields()
        //{
        //    try
        //    {
        //        var fields = await _productService.GetProductAdditionalFieldsAsync();
        //        return Ok(new { fields, ex = (string)null });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok(new { fields = new List<object>(), ex = ex.Message });
        //    }
        //}

        //[HttpGet("images/{productId}")]
        //public async Task<IActionResult> GetProductImages(int productId)
        //{
        //    try
        //    {
        //        var images = await _productService.GetProductImagesAsync(productId);
        //        return Ok(new { images, ex = (string)null });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok(new { images = new List<object>(), ex = ex.Message });
        //    }
        //}

        //[HttpPost("images/array")]
        //public async Task<IActionResult> GetProductsImageArray([FromBody] int[] productIds)
        //{
        //    try
        //    {
        //        var images = await _productService.GetProductsImageArrayAsync(productIds);
        //        return Ok(new { images, ex = (string)null });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok(new { images = new List<object>(), ex = ex.Message });
        //    }
        //}

        //[HttpGet("rest")]
        //public async Task<IActionResult> GetProductsRest()
        //{
        //    try
        //    {
        //        var products = await _productService.GetProductsRestAsync();
        //        return Ok(new { products, ex = (string)null });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok(new { products = new List<object>(), ex = ex.Message });
        //    }
        //}
    }
}
