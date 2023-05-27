using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public class BuildActivityService : IBuildActivityService
{
    private List<PerformedExerciseEntity> _performedExercises = new List<PerformedExerciseEntity>();

    public event Action OnBuildActivityStateChanged;

    public void AddPerformedExercise(PerformedExerciseEntity performedExercise)
    {
        _performedExercises.Add(performedExercise);
        OnBuildActivityStateChanged.Invoke();
    }
    public List<PerformedExerciseEntity> GetPerformedExercises()
    {
        return _performedExercises;
    }
}