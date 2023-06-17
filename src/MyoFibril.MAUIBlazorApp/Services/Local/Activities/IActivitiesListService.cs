using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local.Activities;
public interface IActivitiesListService
{
    Task<List<ActivityEntity>> GetActivitiesAsync();
}