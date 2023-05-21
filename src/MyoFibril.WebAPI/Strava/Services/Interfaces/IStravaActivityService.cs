using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.GetActivity;

namespace MyoFibril.WebAPI.Strava.Services.Interfaces;

public interface IStravaActivityService
{
    Task<GetActivityResponse> GetActivityById(string id);
    Task<CreateActivityResponse> CreateActivity(CreateActivityRequest createActivityRequest);
}
