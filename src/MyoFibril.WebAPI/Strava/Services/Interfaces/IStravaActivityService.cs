using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.GetActivity;

namespace MyoFibril.WebAPI.Strava.Services.Interfaces;

public interface IStravaActivityService
{
    Task<StravaGetActivityResponse> GetActivityById(string id);
    Task<StravaCreateActivityResponse> CreateActivity(StravaCreateActivityRequest createActivityRequest);
}
