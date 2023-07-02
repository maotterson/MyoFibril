using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;
using MyoFibril.WebAPI.Common.Attributes;
using MyoFibril.WebAPI.Common.Utils.Request;
using MyoFibril.WebAPI.Services.Interfaces;
using System.ComponentModel;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly ILogger<ActivityController> _logger;
    private readonly IActivityService _activityService;

    public ActivityController(ILogger<ActivityController> logger, IActivityService activityService)
    {
        _logger = logger;
        _activityService = activityService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActivityResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetActivityResponse>> GetActivityById(string id, [FromQuery(Name = "include-strava"), DefaultValue(false)] bool includeStrava)
    {
        // todo could provide validation for access tokens provided id owner against that of the token

        var response = await _activityService.GetActivityById(id, includeStrava);

        if (response is null)
        {
            _logger.LogError("Response not found");
            return NotFound();
        }
        return Ok(response);
    }

    [HttpGet("by-user/{username}")]
    [UsernameTokenVerification]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActivityResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<GetActivityResponse>>> GetActivitiesForUsername(string username, 
        [FromQuery(Name = "include-strava"), DefaultValue(false)] bool includeStrava,
        [FromQuery(Name = "limit"), DefaultValue(10)] int numActivities)
    {
        try
        {
            // todo could provide validation for access tokens provided id owner against that of the token
            var accessToken = Request.ExtractBearerToken();

            var response = await _activityService.GetActivitiesForUsername(username, accessToken, includeStrava, numActivities);

            if (response is null)
            {
                _logger.LogError("Response not found");
                return NotFound();
            }
            return Ok(response);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateActivityResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateActivity(CreateActivityRequest createActivityRequest)
    {
        try
        {
            // provide access token with request body contents
            var accessToken = Request.ExtractBearerToken();
            createActivityRequest.AccessToken = accessToken;

            var response = await _activityService.CreateActivity(createActivityRequest);

            if (response is null)
            {
                _logger.LogError("Response not found");
                return NotFound();
            }
            return Ok(response);
        }
        catch
        {
            return BadRequest();
        }
    }
}
