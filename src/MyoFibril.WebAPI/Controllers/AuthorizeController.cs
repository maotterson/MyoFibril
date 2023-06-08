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
    public async Task<ActionResult> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest getTokenWithUserCredentialsRequest)
    {
        var response = _authorizeService.GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest);

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
    public async Task<ActionResult> GetAccessTokenWithRefreshToken(AuthorizeTokenRequest authorizeTokenRequest)
    {
        var isAuthorized = _authorizeService.GetAccessTokenWithRefreshToken(authorizeTokenRequest);

        return Ok(true);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AuthorizeToken(AuthorizeTokenRequest authorizeTokenRequest)
    {
        var isAuthorized = _authorizeService.AuthorizeToken(authorizeTokenRequest);

        return Ok(true);
    }
}
