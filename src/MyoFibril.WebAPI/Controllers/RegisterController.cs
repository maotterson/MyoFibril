using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private readonly ILogger<RegisterController> _logger;
    private readonly IAuthorizeService _authorizeService;

    public RegisterController(ILogger<RegisterController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccessTokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterNewUser(RegisterNewUserRequest request)
    {
        try
        {
            var response = _registerService.RegisterNewUser(request);

            if(response is null)
            {
                _logger.LogError("Response not found");
                throw new NullReferenceException();
            }

            return Ok(response);
        }
        catch(Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
