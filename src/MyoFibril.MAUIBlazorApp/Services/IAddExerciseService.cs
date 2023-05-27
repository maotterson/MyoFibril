using MyoFibril.Contracts.WebAPI.Models;

namespace MyoFibril.MAUIBlazorApp.Services;
public interface IAddExerciseService
{
    event Action OnModalStateChanged;
    ExerciseDto SelectedExercise { get; set; }
    void OpenModal();
    void CloseModal();
    bool IsModalOpen();
    List<ExerciseDto> GetExercisesList();
    void AddExerciseToList(ExerciseDto exerciseDto);
}