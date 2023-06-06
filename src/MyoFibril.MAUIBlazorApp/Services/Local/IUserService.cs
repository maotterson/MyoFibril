namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IUserService
{
    bool IsLoggedIn();
    Task<bool> CheckLoginCredentials(string username, string password);
}