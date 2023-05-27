using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IBuildActivityService
{
    event Action OnBuildActivityStateChanged;
    void AddPerformedExercise(PerformedExerciseEntity performedExercise);
}