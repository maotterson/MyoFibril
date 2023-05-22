using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;
using MyoFibril.Domain.Entities;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;
using MyoFibril.WebAPI.Strava.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class ActivityService : IActivityService
{
    private readonly IStravaActivityService _stravaActivityService;
    private readonly IActivityRepository _activityRepository;
    public ActivityService(IStravaActivityService stravaActivityService, IActivityRepository activityRepository)
    {
        _stravaActivityService = stravaActivityService;
        _activityRepository = activityRepository;
    }

    public async Task<CreateActivityResponse> CreateActivity(CreateActivityRequest request)
    {
        // persist to strava
        var stravaRequest = request.AsStravaCreateActivityRequest();
        var stravaResponse = await _stravaActivityService.CreateActivity(stravaRequest);

        // persist to repository
        var activityEntity = new ActivityEntity
        {
            Guid = Guid.NewGuid(),
            Name = request.Name,
            DateCreated = DateTime.Now,
            StravaActivityId = stravaResponse.Id
        };
        var isCreated = await _activityRepository.CreateActivity(activityEntity);
        if (!isCreated) throw new Exception("todo: create activity failed exception message"); // todo better exception message

        // return create activity response
        var createActivityResponse = new CreateActivityResponse
        {
            Id = activityEntity.Guid,
            Name = activityEntity.Name,
            StravaActivity = stravaResponse
        };

        return createActivityResponse;
    }

    public async Task<GetActivityResponse> GetActivityById(string id)
    {
        var activityEntity = await _activityRepository.GetActivityById(id);

        // retrieve connected activity from strava if one exists
        var stravaActivity = activityEntity.StravaActivityId is not null ?
            await _stravaActivityService.GetActivityById(activityEntity.StravaActivityId.ToString()!) :
            null;

        // return get activity response
        var getActivityResponse = new GetActivityResponse
        {
            Id = activityEntity.Guid,
            Name = activityEntity.Name,
            StravaActivity = stravaActivity
        };

        return getActivityResponse;
    }
}
