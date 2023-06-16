using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.GetUser;
using MyoFibril.WebAPI.Services;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetUserByUsername(string username)
    {
        var response = await _userService.GetUserByUsername(username);

        if (response is null)
        {
            _logger.LogError("Response not found");
            return NotFound();
        }
        return Ok(response);
    }
}