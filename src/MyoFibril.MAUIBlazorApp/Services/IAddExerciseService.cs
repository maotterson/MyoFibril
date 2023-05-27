using MyoFibril.Contracts.WebAPI.Models;
using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services;
public interface IAddExerciseService
{
    event Action OnModalStateChanged;
    ExerciseEntity SelectedExercise { get; set; }
    void OpenModal();
    void CloseModal();
    bool IsModalOpen();
    List<ExerciseEntity> GetExercisesList();
    void AddExerciseToList(ExerciseEntity exerciseDto);
}