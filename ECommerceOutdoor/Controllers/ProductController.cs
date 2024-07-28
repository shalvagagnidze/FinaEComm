using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Features.Commands.ProductCommands;
using ServiceLayer.Features.Queries.ProductQueries;
using ServiceLayer.Models;

namespace ECommerceOutdoor.Controllers
{
    public class ProductController : ApiControllerBase
    {
        [HttpGet("get-all-products")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await Mediator.Send(new GetAllProductsQuery());

            return Ok(products);
        }

        [HttpGet("get-products-by-{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await Mediator.Send(new GetProductByIdQuery(id));

            return Ok(product);
        }

        [HttpGet("get-product-by-filtering")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProductByFiltering(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var products = await Mediator.Send(new ProductFilteringQuery(searchTerm, sortColumn, sortOrder, page, pageSize));

            return Ok(products);
        }

        [HttpPost("add-product")]
        [ProducesResponseType(200)]
        [Authorize("Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);

            return Ok(productId);
        }

        [HttpPut("update-product")]
        [ProducesResponseType(200)]
        [Authorize("Admin")]
        public async Task<IActionResult> UpdateProduct(ProductModel model)
        {
            await Mediator.Send(new UpdateProductCommand(model));

            return Ok("Product updated successfully!");
        }

        [HttpPut("delete-product")]
        [ProducesResponseType(200)]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteProduct(ProductModel model)
        {
            await Mediator.Send(new DeleteProductCommand(model));

            return Ok("Product deleted successfully!");
        }


        [HttpPut("delete-product-by-{id}")]
        [ProducesResponseType(200)]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteProductById(Guid id)
        {
            await Mediator.Send(new DeleteProductByIdCommand(id));

            return Ok("Product deleted successfully!");
        }
    }
}
