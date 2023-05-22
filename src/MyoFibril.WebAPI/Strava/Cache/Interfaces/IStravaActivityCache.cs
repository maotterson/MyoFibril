using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.GetActivity;
using MyoFibril.Contracts.Strava.Models;

namespace MyoFibril.WebAPI.Strava.Cache.Interfaces;

public interface IStravaActivityCache
{
    Task<StravaDetailedActivity> GetActivityById(string id);
    Task<StravaDetailedActivity> AddActivityToCache(StravaDetailedActivity activity);
    Task<StravaDetailedActivity> RemoveActivityFromCache(StravaDetailedActivity activity);
    Task<IEnumerable<StravaDetailedActivity>> GetAllActivities();

}
