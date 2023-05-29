using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.Models;
using MyoFibril.Domain.Entities;
using MyoFibril.MAUIBlazorApp.Services.Api;

namespace MyoFibril.MAUIBlazorApp.Components.CreateActivity;
public class CreateActivityViewModel
{
    private readonly INewActivityService _newActivityService;
    public readonly List<ExerciseDto> Exercises = new List<ExerciseDto>();
    public CreateActivityViewModel(INewActivityService newActivityService)
    {
        _newActivityService = newActivityService;
    }
    public async Task CreateActivity(ActivityEntity activity)
    {
        var createActivityRequest = new CreateActivityRequest
        {
            Name = activity.Name
        };
        var createdActivity = await _newActivityService.CreateActivity(createActivityRequest);
    }
}