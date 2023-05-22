using MyoFibril.Contracts.Strava.Models;
using MyoFibril.WebAPI.Strava.Cache.Interfaces;

namespace MyoFibril.WebAPI.Strava.Cache;

public class StravaActivityCache : IStravaActivityCache
{
    public Task<StravaDetailedActivity> AddActivityToCache(StravaDetailedActivity activity)
    {
        throw new NotImplementedException();
    }

    public Task<StravaDetailedActivity> GetActivityById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StravaDetailedActivity>> GetAllActivities()
    {
        throw new NotImplementedException();
    }

    public Task<StravaDetailedActivity> RemoveActivityFromCache(StravaDetailedActivity activity)
    {
        throw new NotImplementedException();
    }
}
