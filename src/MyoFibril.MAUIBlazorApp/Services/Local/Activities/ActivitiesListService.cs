using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local.Activities;
public class ActivitiesListService : IActivitiesListService
{
    private readonly IUserService _userService;
    public ActivitiesListService(IUserService userService)
    {

        _userService = userService;

    }
    public Task<List<ActivityEntity>> GetActivitiesAsync()
    {
        var username = _userService.GetLoggedInUser();
    }
}