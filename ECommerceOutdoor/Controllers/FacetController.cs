using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Features.Commands.FacetCommands;
using ServiceLayer.Models;


namespace ECommerceOutdoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacetController : ApiControllerBase
    {
        [HttpPost("add-facet")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> CreateFacet(CreateFacetCommand command)
        {
            var facetId = await Mediator.Send(command);

            return Ok(facetId);
        }

        [HttpPut("update-facet")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateFacet(FacetModel model)
        {
            await Mediator.Send(new UpdateFacetCommand(model));

            return Ok();
        }

        [HttpDelete("delete-facet")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> DeleteFacet(FacetModel model)
        {
            await Mediator.Send(new DeleteFacetCommand(model));

            return Ok();
        }
    }
}
