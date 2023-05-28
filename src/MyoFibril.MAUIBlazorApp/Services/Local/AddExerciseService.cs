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
    private List<ExerciseEntity> _allExercises;
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
    public async Task LoadExerciseListAsync()
    {
        _allExercises = await _exerciseDataLoader.LoadExerciseList();
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
        if (_allExercises is null) await LoadExerciseListAsync();
        return _allExercises;
    }

    public async Task AddExerciseToList(ExerciseEntity exercise)
    {
        _allExercises.Add(exercise);
        OnModalStateChanged.Invoke();
    }

    public void CancelSelectedExercise()
    {
        SelectedExercise = NO_EXERCISE_SELECTED;
    }

}