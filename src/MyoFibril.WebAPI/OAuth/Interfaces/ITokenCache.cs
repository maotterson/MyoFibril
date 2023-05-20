namespace MyoFibril.WebAPI.OAuth.Interfaces;
public interface ITokenCache
{
    void SetAccessToken(string accessToken);
    string GetAccessToken();
}