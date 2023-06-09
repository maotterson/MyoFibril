using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest request)
    {
        try
        {
            var response = await _authorizeService.GetAccessTokenWithUserCredentials(request);

            if(response is null)
            {
                _logger.LogError("Response not found");
                throw new NullReferenceException();
            }

            return Ok(response);
        }
        catch (Exception ex) when (ex is InvalidAuthorizeTokenRequestException ||
                                    ex is InvalidRefreshTokenException ||
                                    ex is InvalidCredentialsException)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest();
        }

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccessTokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAccessTokenWithRefreshToken(GetTokenWithRefreshTokenRequest request)
    {
        try
        {
            var response = await _authorizeService.GetAccessTokenWithRefreshToken(request);

            if (response is null)
            {
                _logger.LogError("Response not found");
                throw new NullReferenceException();
            }

            return Ok(response);
        }
        catch (Exception ex) when (ex is InvalidRefreshTokenException) 
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest();
        }
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorizeTokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AuthorizeToken(AuthorizeTokenRequest request)
    {
        try
        {
            var response = await _authorizeService.AuthorizeToken(request);

            if (response is null)
            {
                _logger.LogError("Response not found");
                throw new NullReferenceException();
            }

            return Ok(response);
        }
        catch (Exception ex) when (ex is InvalidAuthorizeTokenRequestException || 
                                    ex is InvalidRefreshTokenException || 
                                    ex is InvalidCredentialsException)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest();
        }

    }
}
