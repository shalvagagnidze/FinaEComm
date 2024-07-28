using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Features.Commands.CategoryCommands;
using ServiceLayer.Features.Queries.CategoryQueries;
using ServiceLayer.Models;

namespace ECommerceOutdoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ApiControllerBase
    {
        [HttpGet("get-all-categories")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await Mediator.Send(new GetAllCategoriesQuery());

            return Ok(categories);
        }

        [HttpGet("get-category-by-{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await Mediator.Send(new GetCategoryByIdQuery(id));

            return Ok(category);
        }

        [HttpGet("get-category-by-filtering")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCategoryByFiltering(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var categories = await Mediator.Send(new CategoryFilteringQuery(searchTerm, sortColumn, sortOrder, page, pageSize));

            return Ok(categories);
        }

        [HttpPost("add-category")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var categoryId = await Mediator.Send(command);

            return Ok(categoryId);
        }

        [HttpPut("update-category")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateCategory(CategoryModel model)
        {
            await Mediator.Send(new UpdateCategoryCommand(model));

            return Ok("Category updated successfully!");
        }

        [HttpPut("delete-category")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(CategoryModel model)
        {
            await Mediator.Send(new DeleteCategoryCommand(model));

            return Ok("Category deleted successfully!");
        }

        [HttpPut("delete-category-by-{id}")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategoryById(Guid id)
        {
            await Mediator.Send(new DeleteCategoryByIdCommand(id));

            return Ok($"Category with Id:{id} deleted successfully!");
        }

    }
}
