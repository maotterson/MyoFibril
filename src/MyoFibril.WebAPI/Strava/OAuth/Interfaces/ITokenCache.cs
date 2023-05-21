namespace MyoFibril.WebAPI.Strava.OAuth.Interfaces;
public interface ITokenCache
{
    void SetAccessToken(string accessToken);
    string GetAccessToken();
}