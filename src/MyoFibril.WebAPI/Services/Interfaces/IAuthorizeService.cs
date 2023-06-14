using MyoFibril.Contracts.WebAPI.Auth;

namespace MyoFibril.WebAPI.Services.Interfaces;

public interface IAuthorizeService
{
    Task<AuthorizeTokenResponse> AuthorizeToken(AuthorizeTokenRequest authorizeTokenRequest);
    Task<GetAccessTokenResponse> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest request);
    Task<GetAccessTokenResponse> GetAccessTokenWithRefreshToken(GetTokenWithRefreshTokenRequest request);
    Task<LogoutResponse> Logout(LogoutRequest logoutRequest);
}
