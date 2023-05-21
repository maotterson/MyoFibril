using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.WebAPI.Api;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly ILogger<ActivityController> _logger;
    private readonly IStravaApi _api;

    public ActivityController(ILogger<ActivityController> logger, IStravaApi api)
    {
        _logger = logger;
        _api = api;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActivityResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetActivityResponse>> GetActivityById(string id)
    {
        var response = await _api.GetActivityById(id);

        if (response is null)
        {
            _logger.LogError("Response not found");
            return NotFound();
        }
        return Ok(response.Content);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateActivityResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateActivity(CreateActivityRequest createActivityRequest)
    {
        var response = await _api.CreateActivity(createActivityRequest);

        if (response is null)
        {
            _logger.LogError("Response not found");
            return NotFound();
        }
        return Ok(response.Content);
    }
}
