namespace MyoFibril.WebAPI.OAuth;
public interface ITokenCache
{
    void SetAccessToken(string accessToken);
    string GetAccessToken();
}