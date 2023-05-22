using MyoFibril.Domain.Entities;
using MyoFibril.WebAPI.Repositories.Interfaces;

namespace MyoFibril.WebAPI.Repositories;

public class ActivityInMemoryRepository : IActivityRepository
{
    private readonly List<ActivityEntity> _activities = new List<ActivityEntity>();
    public async Task<bool> CreateActivity(ActivityEntity activityEntity)
    {
        try
        {
            _activities.Add(activityEntity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ActivityEntity> GetActivityById(string id)
    {
        var activity = _activities.FirstOrDefault(a => a.Guid.ToString() == id);
        if (activity is null) throw new Exception($"Activity {id} not found"); // todo more specific exception

        return activity;
    }
}
