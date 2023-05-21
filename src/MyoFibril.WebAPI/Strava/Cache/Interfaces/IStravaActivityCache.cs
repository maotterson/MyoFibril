using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.GetActivity;
using MyoFibril.Contracts.Strava.Models;

namespace MyoFibril.WebAPI.Strava.Cache.Interfaces;

public interface IStravaActivityCache
{
    Task<DetailedActivity> GetActivityById(string id);
    Task<DetailedActivity> AddActivityToCache(DetailedActivity activity);
    Task<DetailedActivity> RemoveActivityFromCache(DetailedActivity activity);
    Task<IEnumerable<DetailedActivity>> GetAllActivities();

}
