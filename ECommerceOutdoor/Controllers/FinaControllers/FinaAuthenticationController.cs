using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Models.FinaModels.Auth;
using ServiceLayer.Services.FinaServices.FinaHelpers;

namespace ECommerceOutdoor.Controllers.FinaControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinaAuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly FinaApiClient _apiClient;

        public FinaAuthenticationController(IAuthenticationService authService, FinaApiClient apiClient)
        {
            _authService = authService;
            _apiClient = apiClient;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            try
            {
                var response = await _authService.AuthenticateAsync(request.Login!, request.Password!);

                if (response.Ex != null)
                {
                    return Ok(new AuthenticationResponse { Token = null, Ex = response.Ex });
                }

                _apiClient.SetAuthToken(response.Token!);

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return Ok(new AuthenticationResponse { Token = null, Ex = ex.Message });
            }
        }
    }
}
