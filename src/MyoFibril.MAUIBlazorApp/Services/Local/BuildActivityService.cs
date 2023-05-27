using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public class BuildActivityService : IBuildActivityService
{
    private List<PerformedExerciseEntity> _performedExercises = new List<PerformedExerciseEntity>();
    public void AddPerformedExercise(PerformedExerciseEntity performedExercise)
    {
        // todo
    }
}