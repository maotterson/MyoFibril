namespace MyoFibril.MAUIBlazorApp.Services;
public class AddExerciseService : IAddExerciseService
{
    private bool _modalOpen = false;
    public event Action OnModalStateChanged;
    public void OpenModal()
    {
        _modalOpen = true;
        OnModalStateChanged.Invoke();
    }
    public void CloseModal()
    {
        _modalOpen = false;
        OnModalStateChanged.Invoke();
    }
    public bool IsModalOpen()
    {
        return _modalOpen;
    }

}