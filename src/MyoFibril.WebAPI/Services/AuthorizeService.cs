using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class AuthorizeService : IAuthorizeService
{
    public Task<bool> AuthorizeToken(AuthorizeTokenRequest authorizeTokenRequest)
    {
        var token = authorizeTokenRequest.AccessToken;

    }

    public Task<GetAccessTokenResponse> GetAccessTokenWithRefreshToken(GetTokenWithRefreshTokenRequest request)
    {
        var refreshToken = request.RefreshToken;
    }

    public Task<GetAccessTokenResponse> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest request)
    {
        var salt = ""; // todo: retrieve user-specific salt
        var userCredentials = new ProtectedUserCredentials(request.Username, request.Password, salt);
    }
}
