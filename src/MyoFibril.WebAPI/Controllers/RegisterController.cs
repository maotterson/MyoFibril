using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.WebAPI.Services;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private readonly ILogger<RegisterController> _logger;
    private readonly IRegisterService _registerService;

    public RegisterController(ILogger<RegisterController> logger, IRegisterService registerService)
    {
        _logger = logger;
        _registerService = registerService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccessTokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterNewUser(RegisterNewUserRequest request)
    {
        try
        {
            var response = await _registerService.RegisterNewUser(request);

            if(response is null)
            {
                _logger.LogError("Response not found");
                throw new NullReferenceException();
            }

            return Ok(response);
        }
        catch (Exception ex) when (ex is InvalidAccessTokenException ||
                                    ex is InvalidRefreshTokenException ||
                                    ex is InvalidCredentialsException ||
                                    ex is UsernameExistsException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}
