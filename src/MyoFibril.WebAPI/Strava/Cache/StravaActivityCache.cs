using MyoFibril.Contracts.Strava.Models;
using MyoFibril.WebAPI.Strava.Cache.Interfaces;

namespace MyoFibril.WebAPI.Strava.Cache;

public class StravaActivityCache : IStravaActivityCache
{
    public Task<DetailedActivity> AddActivityToCache(DetailedActivity activity)
    {
        throw new NotImplementedException();
    }

    public Task<DetailedActivity> GetActivityById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DetailedActivity>> GetAllActivities()
    {
        throw new NotImplementedException();
    }

    public Task<DetailedActivity> RemoveActivityFromCache(DetailedActivity activity)
    {
        throw new NotImplementedException();
    }
}
