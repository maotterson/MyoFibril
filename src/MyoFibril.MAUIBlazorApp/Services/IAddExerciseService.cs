using MyoFibril.Contracts.WebAPI.Models;
using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services;
public interface IAddExerciseService
{
    // properties
    bool IsExerciseSelected { get; }
    ExerciseEntity SelectedExercise { get; set; }
    event Action OnModalStateChanged;

    // modal
    void OpenModal();
    void CloseModal();
    bool IsModalOpen();

    // methods
    Task<List<ExerciseEntity>> GetExercisesListAsync();
    Task AddExerciseToList(ExerciseEntity exerciseDto);
}