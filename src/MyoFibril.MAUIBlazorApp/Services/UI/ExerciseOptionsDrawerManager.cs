using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public class ExerciseOptionsDrawerManager : IExerciseOptionsDrawerManager
{
    private PerformedExerciseEntity _performedExercise = null;

    public event Action OnDrawerStateChanged;

    public void OpenDrawer(PerformedExerciseEntity performedExercise)
    {
        _performedExercise = performedExercise;
        OnDrawerStateChanged?.Invoke();
    }

    public bool IsDrawerOpen()
    {
        return _performedExercise is not null;
    }

    public void CloseDrawer()
    {
        _performedExercise = null;
        OnDrawerStateChanged?.Invoke();
    }
}