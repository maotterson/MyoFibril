namespace MyoFibril.MAUIBlazorApp.Services;
public interface IAddExerciseService
{
    event Action OnModalStateChanged;
    string SelectedExercise { get; set; }
    void OpenModal();
    void CloseModal();
    bool IsModalOpen();
    List<string> GetExercisesList();
    void AddExerciseToList(string exercise);
}