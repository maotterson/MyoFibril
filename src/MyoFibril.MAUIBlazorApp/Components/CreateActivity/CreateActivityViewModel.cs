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
    public async Task<CreateActivityResponse> CreateActivity(ActivityEntity activity)
    {
        var createActivityRequest = new CreateActivityRequest
        {
            Name = activity.Name,
            DateCreated = activity.DateCreated,
            PerformedExercises = activity.PerformedExercises
        };

        // using entity, if necessary can implement CreateActivityDto
        var createdActivity = await _newActivityService.CreateActivity(createActivityRequest);
        return createdActivity;
    }
}