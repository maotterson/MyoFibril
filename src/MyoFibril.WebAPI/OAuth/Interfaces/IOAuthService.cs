namespace MyoFibril.WebAPI.OAuth.Interfaces;

public interface IOAuthService
{
    Task<string> GetAccessToken();
}