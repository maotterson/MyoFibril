namespace MyoFibril.MAUIBlazorApp.Services.UI;
public interface IModalService
{
    event Action OnModalStateChanged;
    void OpenModal();
    void CloseModal();
    bool IsModalOpen();
}