namespace MyoFibril.WebAPI.OAuth;

public interface ITokenRefreshService
{
    Task<TokenResponse> RefreshAccessToken();
}