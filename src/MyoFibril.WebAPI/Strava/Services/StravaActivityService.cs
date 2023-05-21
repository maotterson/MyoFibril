using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.GetActivity;
using MyoFibril.WebAPI.Strava.Api;
using MyoFibril.WebAPI.Strava.Cache.Interfaces;
using MyoFibril.WebAPI.Strava.Services.Interfaces;

namespace MyoFibril.WebAPI.Strava.Services;

public class StravaActivityService : IStravaActivityService
{
    private readonly ILogger<StravaActivityService> _logger;
    private readonly IStravaApi _api;
    private readonly IStravaActivityCache _activityCache;

    public StravaActivityService(ILogger<StravaActivityService> logger, IStravaApi api, IStravaActivityCache activityCache)
    {
        _logger = logger;
        _api = api;
        _activityCache = activityCache;
    }

    public async Task<CreateActivityResponse> CreateActivity(CreateActivityRequest createActivityRequest)
    {
        var response = await _api.CreateActivity(createActivityRequest);
        if (response is null || !response.IsSuccessStatusCode)
        {
            var message = $" Activity not created: ${response?.ReasonPhrase}";
            _logger.LogError(message);
            throw new Exception(message); // todo specific exceptions
        }
        return response.Content;
    }

    public async Task<GetActivityResponse> GetActivityById(string id)
    {
        var response = await _api.GetActivityById(id);
        if (response is null || !response.IsSuccessStatusCode)
        {
            var message = $" Activity ${id} not found: ${response?.ReasonPhrase}";
            _logger.LogError(message);
            throw new Exception(message); // todo specific exceptions
        }
        return response.Content;
    }
}
