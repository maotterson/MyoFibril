using Microsoft.AspNetCore.Mvc;
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
        var response = await _activityService.GetActivityById(id, includeStrava);

        if (response is null)
        {
            _logger.LogError("Response not found");
            return NotFound();
        }
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateActivityResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateActivity(CreateActivityRequest createActivityRequest)
    {
        try
        {
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
