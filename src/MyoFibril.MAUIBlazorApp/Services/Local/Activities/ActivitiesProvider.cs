using MyoFibril.Domain.Entities;
using MyoFibril.MAUIBlazorApp.Data.Activities;

namespace MyoFibril.MAUIBlazorApp.Services.Local.Activities;
public class ActivitiesProvider : IActivitiesProvider
{
    private readonly IUserService _userService;
    private readonly IActivitiesRepository _activitiesRepository;
    public ActivitiesProvider(IUserService userService, IActivitiesRepository activitiesRepository)
    {
        _userService = userService;
    }
    public Task<List<ActivityEntity>> GetActivitiesAsync()
    {
        var username = _userService.GetLoggedInUser();
        if (username is null) throw new NullReferenceException(nameof(username));
        throw new NotImplementedException(); // todo implement getting of activities
    }
}