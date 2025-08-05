using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Features.Commands.BrandCommands;
using ServiceLayer.Features.Commands.FAQCommands;
using ServiceLayer.Features.Queries.BrandQueries;
using ServiceLayer.Features.Queries.FAQQueries;
using ServiceLayer.Models;

namespace ECommerceOutdoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FAQController : ApiControllerBase
    {
        [HttpGet("get-all-faqs")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllFAQs()
        {
            var faqs = await Mediator.Send(new GetAllFAQsQuery());

            return Ok(faqs);
        }

        [HttpGet("get-faq-by-{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetFAQByIds(Guid id)
        {
            var faq = await Mediator.Send(new GetFAQByIdQuery(id));

            return Ok(faq);
        }

        [HttpPost("add-faq")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> CreateFAQ(CreateFAQCommand command)
        {
            var faqId = await Mediator.Send(command);

            return Ok(faqId);
        }

        [HttpPut("update-faq")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateFAQ(FAQModel model)
        {
            await Mediator.Send(new UpdateFAQCommand(model));

            return Ok("FAQ updated successfully!");
        }

        [HttpDelete("delete-faq")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFAQ(FAQModel model)
        {
            await Mediator.Send(new DeleteFAQCommand(model));

            return Ok("FAQ deleted successfully!");
        }

        [HttpDelete("delete-faq-by-{id}")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFAQById(Guid id)
        {
            await Mediator.Send(new DeleteFAQByIdCommand(id));

            return Ok($"FAQ with Id:{id} deleted successfully!");
        }
    }
}
