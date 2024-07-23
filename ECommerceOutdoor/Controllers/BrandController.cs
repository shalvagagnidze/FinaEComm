using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Features.Commands.BrandCommands;
using ServiceLayer.Features.Queries.BrandQueries;
using ServiceLayer.Models;

namespace ECommerceOutdoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ApiControllerBase
    {
        [HttpGet("get-all-brands")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await Mediator.Send(new GetAllBrandsQuery());

            return Ok(brands);
        }

        [HttpGet("get-brand-by-{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBrandByIds(Guid id)
        {
            var brand = await Mediator.Send(new GetBrandByIdQuery(id));

            return Ok(brand);
        }

        [HttpPost("add-brand")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateBrand(CreateBrandCommand command)
        {
            var brandId = await Mediator.Send(command);

            return Ok(brandId);
        }

        [HttpPut("update-brand")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateBrand(BrandModel model)
        {
            await Mediator.Send(new UpdateBrandCommand(model));

            return Ok("Brand updated successfully!");
        }

        [HttpPut("delete-brand")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteBrand(BrandModel model)
        {
            await Mediator.Send(new DeleteBrandCommand(model));

            return Ok("Brand deleted successfully!");
        }

        [HttpPut("delete-brand-by-{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteBrandById(Guid id)
        {
            await Mediator.Send(new DeleteBrandByIdCommand(id));

            return Ok($"Brand with Id:{id} deleted successfully!");
        }
    }
}
