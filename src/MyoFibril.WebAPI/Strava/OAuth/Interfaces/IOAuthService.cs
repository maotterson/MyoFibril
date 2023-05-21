namespace MyoFibril.WebAPI.Strava.OAuth.Interfaces;

public interface IOAuthService
{
    Task<string> GetAccessToken();
}