using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.WebAPI.Services.Interfaces;
using System.ComponentModel;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly ILogger<AuthorizeController> _logger;
    private readonly IAuthorizeService _authorizeService;

    public AuthorizeController(ILogger<AuthorizeController> logger, IAuthorizeService authorizeService)
    {
        _logger = logger;
        _authorizeService = authorizeService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccessTokenResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest request)
    {
        var response = await _authorizeService.GetAccessTokenWithUserCredentials(request);

        if(response is null)
        {
            _logger.LogError("Response not found");
            return NotFound();
        }


        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccessTokenWithRefreshToken(GetTokenWithRefreshTokenRequest request)
    {
        var isAuthorized = await _authorizeService.GetAccessTokenWithRefreshToken(request);

        return Ok(true);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AuthorizeToken(AuthorizeTokenRequest request)
    {
        var isAuthorized = await _authorizeService.AuthorizeToken(request);

        return Ok(true);
    }
}
