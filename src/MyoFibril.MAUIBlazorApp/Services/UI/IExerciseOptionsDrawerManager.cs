using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public interface IExerciseOptionsDrawerManager
{
    public PerformedExerciseEntity SelectedExercise { get; }
    event Action OnDrawerStateChanged;
    bool IsDrawerOpen();
    void OpenDrawer(PerformedExerciseEntity exercise);
    void CloseDrawer();
    void ToggleDrawer(PerformedExerciseEntity exercise);
}