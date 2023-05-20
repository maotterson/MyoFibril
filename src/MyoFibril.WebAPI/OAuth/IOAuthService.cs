namespace MyoFibril.WebAPI.OAuth;

public interface IOAuthService
{
    Task<string> GetAccessToken();
}