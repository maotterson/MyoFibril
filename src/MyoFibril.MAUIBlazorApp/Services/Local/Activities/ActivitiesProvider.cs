using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local.Activities;
public class ActivitiesProvider : IActivitiesProvider
{
    private readonly IUserService _userService;
    public ActivitiesProvider(IUserService userService)
    {

        _userService = userService;

    }
    public Task<List<ActivityEntity>> GetActivitiesAsync()
    {
        var username = _userService.GetLoggedInUser();
        throw new NotImplementedException(); // todo implement getting of activities
    }
}