using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Data.Activities;
public interface IActivitiesRepository
{
    Task<List<ActivityEntity>> GetActivitiesByUsernameAsync(string username, int numActivities);
}
