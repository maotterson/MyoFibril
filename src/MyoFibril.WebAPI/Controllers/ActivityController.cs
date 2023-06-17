﻿using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;
using MyoFibril.WebAPI.Services.Interfaces;
using MyoFibril.WebAPI.Utils.Request;
using System.ComponentModel;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
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

    [HttpGet("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActivityResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<GetActivityResponse>>> GetActivitiesForUsername(string username, [FromQuery(Name = "include-strava"), DefaultValue(false)] bool includeStrava)
    {
        try
        {
            // todo could provide validation for access tokens provided id owner against that of the token
            var accessToken = Request.ExtractBearerToken();

            var response = await _activityService.GetActivitiesForUsername(username, accessToken);

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
