using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IBuildActivityService
{
    void AddPerformedExercise(PerformedExerciseEntity performedExercise);
}