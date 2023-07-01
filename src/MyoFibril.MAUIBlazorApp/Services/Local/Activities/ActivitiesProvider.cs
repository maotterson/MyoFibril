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
        _activitiesRepository = activitiesRepository;
    }
    public async Task<List<ActivityEntity>> GetActivitiesAsync()
    {
        var user = await _userService.GetLoggedInUser();
        if (user is null) throw new NullReferenceException(nameof(user));
        var activities = await _activitiesRepository.GetActivitiesByUsernameAsync(user.Username);

        return activities;
    }
}