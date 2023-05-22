using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;
using MyoFibril.WebAPI.Services.Interfaces;
using MyoFibril.WebAPI.Strava.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class ActivityService : IActivityService
{
    private readonly IStravaActivityService _stravaActivityService;
    public ActivityService(IStravaActivityService stravaActivityService)
    {
        _stravaActivityService = stravaActivityService;
    }

    public async Task<CreateActivityResponse> CreateActivity(CreateActivityRequest request)
    {
        var stravaRequest = request.AsStravaCreateActivityRequest();
        var stravaResponse = await _stravaActivityService.CreateActivity(stravaRequest);
        var response = new CreateActivityResponse
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            StravaActivity = stravaResponse,
        };
        return response;
    }

    public async Task<GetActivityResponse> GetActivityById(string id)
    {
    }
}
