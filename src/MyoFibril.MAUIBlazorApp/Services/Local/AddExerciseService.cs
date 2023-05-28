using AudioUnit;
using MyoFibril.Contracts.WebAPI.Models;
using MyoFibril.Domain.Entities;
using MyoFibril.SharedData.Loaders;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public class AddExerciseService : IAddExerciseService
{
    private bool _modalOpen = false;
    public bool IsExerciseSelected => _selectedExercise == NO_EXERCISE_SELECTED;
    public event Action OnModalStateChanged;

    private ExerciseDataLoader _exerciseDataLoader;
    private List<ExerciseEntity> _exercises;
    private static ExerciseEntity NO_EXERCISE_SELECTED = new ExerciseEntity();
    private ExerciseEntity _selectedExercise = NO_EXERCISE_SELECTED;
    public ExerciseEntity SelectedExercise
    {
        get => _selectedExercise;
        set
        {
            _selectedExercise = value;
            OnModalStateChanged.Invoke();
        }
    }

    public AddExerciseService(ExerciseDataLoader exerciseDataLoader)
    {
        _exerciseDataLoader = exerciseDataLoader;
    }
    public async void LoadDataAsync()
    {
        await _exerciseDataLoader.Load();
    }
    public void OpenModal()
    {
        _modalOpen = true;
        OnModalStateChanged.Invoke();
    }
    public void CloseModal()
    {
        CancelSelectedExercise();
        _modalOpen = false;
        OnModalStateChanged.Invoke();
    }
    public bool IsModalOpen()
    {
        return _modalOpen;
    }

    public async Task<List<ExerciseEntity>> GetExercisesListAsync()
    {
        return _exercises;
    }

    public async Task AddExerciseToList(ExerciseEntity exercise)
    {
        _exercises.Add(exercise);
        OnModalStateChanged.Invoke();
    }

    public void CancelSelectedExercise()
    {
        SelectedExercise = NO_EXERCISE_SELECTED;
    }

}