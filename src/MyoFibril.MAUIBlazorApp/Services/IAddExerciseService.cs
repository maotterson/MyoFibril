using MyoFibril.Contracts.WebAPI.Models;
using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services;
public interface IAddExerciseService
{
    event Action OnModalStateChanged;
    void OpenModal();
    void CloseModal();
    bool IsModalOpen();
    ExerciseEntity SelectedExercise { get; set; }
    Task<List<ExerciseEntity>> GetExercisesListAsync();
    Task AddExerciseToList(ExerciseEntity exerciseDto);
}