using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public interface IExerciseOptionsDrawerManager
{
    bool IsDrawerOpen();
    void OpenDrawer(PerformedExerciseEntity exercise);
    void CloseDrawer();
}