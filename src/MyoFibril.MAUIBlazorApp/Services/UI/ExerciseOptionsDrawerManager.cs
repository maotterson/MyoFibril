using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public class ExerciseOptionsDrawerManager : IExerciseOptionsDrawerManager
{
    public PerformedExerciseEntity SelectedExercise { get; private set; }

    public event Action OnDrawerStateChanged;

    public void OpenDrawer(PerformedExerciseEntity performedExercise)
    {
        SelectedExercise = performedExercise;
        OnDrawerStateChanged?.Invoke();
    }

    public void ToggleDrawer(PerformedExerciseEntity performedExercise)
    {
        if (IsDrawerOpen())
        {
            CloseDrawer();
            return;
        }
        OpenDrawer(performedExercise);
    }

    public bool IsDrawerOpen()
    {
        return SelectedExercise is not null;
    }

    public void CloseDrawer()
    {
        SelectedExercise = null;
        OnDrawerStateChanged?.Invoke();
    }
}