using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Models.FinaModels;

namespace ECommerceOutdoor.Controllers.FinaControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinaProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;

        public FinaProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductGroups()
        {
            try
            {
                var groups = await _productGroupService.GetProductGroupsAsync();
                return Ok(new { groups, ex = "" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { groups = new List<FinaProductGroupModel>(), ex = ex.Message });
            }
        }

        //[HttpGet("web")]
        //public async Task<IActionResult> GetWebProductGroups()
        //{
        //    try
        //    {
        //        var groups = await _productGroupService.GetWebProductGroupsAsync();
        //        return Ok(new { groups, ex = (string)null });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok(new { groups = new List<FinaProductGroupModel>(), ex = ex.Message });
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductGroup(int id)
        {
            try
            {
                var group = await _productGroupService.GetProductGroupByIdAsync(id);
                if (group == null)
                {
                    return Ok(new { group = (FinaProductGroupModel?)null, ex = "Group not found" });
                }
                return Ok(new { group, ex = "" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { group = (FinaProductGroupModel?)null, ex = ex.Message });
            }
        }

        [HttpGet("children/{parentId}")]
        public async Task<IActionResult> GetChildGroups(int parentId)
        {
            try
            {
                var groups = await _productGroupService.GetChildGroupsAsync(parentId);
                return Ok(new { groups, ex = "" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { groups = new List<FinaProductGroupModel>(), ex = ex.Message });
            }
        }
    }
}
