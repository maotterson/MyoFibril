using MyoFibril.Contracts.WebAPI.Models;

namespace MyoFibril.MAUIBlazorApp.Services;
public class AddExerciseService : IAddExerciseService
{
    private List<ExerciseDto> _exercises;
    private bool _modalOpen = false;
    public event Action OnModalStateChanged;
    private ExerciseDto _selectedExercise = new ExerciseDto();
    public string SelectedExercise 
    {
        get => _selectedExercise;
        set
        {
            _selectedExercise = value;
            OnModalStateChanged.Invoke();
        }
    }
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

    public List<string> GetExercisesList()
    {
        return _exercises;
    }

    public void AddExerciseToList(string exercise)
    {
        _exercises.Add(exercise);
        OnModalStateChanged.Invoke();
    }
    
}