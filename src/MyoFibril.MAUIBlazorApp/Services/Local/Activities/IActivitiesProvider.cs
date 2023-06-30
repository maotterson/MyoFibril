using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local.Activities;
public interface IActivitiesProvider
{
    Task<List<ActivityEntity>> GetActivitiesAsync();
}