using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IBuildActivityService
{
    event Action OnBuildActivityStateChanged;
    List<PerformedExerciseEntity> GetPerformedExercises();
    void AddPerformedExercise(PerformedExerciseEntity performedExercise);
}