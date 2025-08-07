using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Models.FinaModels.Facets;

namespace ECommerceOutdoor.Controllers.FinaControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinaCharacteristicController : ControllerBase
    {
        private readonly ICharacteristicService _characteristicService;

        public FinaCharacteristicController(ICharacteristicService characteristicService)
        {
            _characteristicService = characteristicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacteristics()
        {
            try
            {
                var characteristics = await _characteristicService.GetCharacteristicsAsync();
                return Ok(new { characteristics, ex = "" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { characteristics = new List<FinaCharacteristicModel>(), ex = ex.Message });
            }
        }

        [HttpGet("values")]
        public async Task<IActionResult> GetCharacteristicValues()
        {
            try
            {
                var values = await _characteristicService.GetCharacteristicValuesAsync();
                return Ok(new { characteristic_values = values, ex = "" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { characteristic_values = new List<FinaProductCharacteristicModel>(), ex = ex.Message });
            }
        }
    }
}
