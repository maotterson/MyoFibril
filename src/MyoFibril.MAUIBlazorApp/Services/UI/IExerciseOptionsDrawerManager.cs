using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public interface IExerciseOptionsDrawerManager
{
    event Action OnDrawerStateChanged;
    bool IsDrawerOpen();
    void OpenDrawer(PerformedExerciseEntity exercise);
    void CloseDrawer();
}