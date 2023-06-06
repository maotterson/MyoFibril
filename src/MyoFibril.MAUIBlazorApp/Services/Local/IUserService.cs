namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IUserService
{
    Task Logout();
    Task<bool> CheckLoginCredentials(string username, string password);
}