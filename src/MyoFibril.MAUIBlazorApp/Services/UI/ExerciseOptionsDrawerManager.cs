using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public class ExerciseOptionsDrawerManager : IExerciseOptionsDrawerManager
{
    private PerformedExerciseEntity _performedExercise = null;

    public void OpenDrawer(PerformedExerciseEntity performedExercise)
    {
        _performedExercise = performedExercise;
    }

    public bool IsDrawerOpen()
    {
        return _performedExercise is not null;
    }

    public void CloseDrawer()
    {
        _performedExercise = null;
    }
}