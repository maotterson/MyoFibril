using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class AuthorizeService : IAuthorizeService
{
    public IAuthorizeRepository _authorizeRepository;

    public AuthorizeService(IAuthorizeRepository authorizeRepository)
    {
        _authorizeRepository = authorizeRepository;
    }

    public async Task<AuthorizeTokenResponse> AuthorizeToken(AuthorizeTokenRequest authorizeTokenRequest)
    {
        var tokenToAuthorize = authorizeTokenRequest.AccessToken;
        var refreshTokenToAuthorize = authorizeTokenRequest.RefreshToken;

        GetAccessTokenResponse? tokenInfo = default;
        UserInfo? userInfo = default;
        var isVerified = _authorizeRepository.isVerifiedToken(tokenToAuthorize);

        if (isVerified)
        {
            tokenInfo = new GetAccessTokenResponse
            {
                AccessToken = tokenToAuthorize,
                RefreshToken = refreshTokenToAuthorize
            };
            userInfo = new UserInfo
            {
                Username = "todo: get username from user repository"
            };
        }

        var accessToken = _authorizeRepository.GetAccessTokenWithRefreshToken(refreshTokenToAuthorize);
        if(accessToken is not null)
        {
            isVerified = true;
            tokenInfo = new GetAccessTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenToAuthorize
            };
            userInfo = new UserInfo
            {
                Username = "todo: get username from user repository"
            };
        }

        return new AuthorizeTokenResponse
        {
            IsAuthorized = isVerified,
            TokenInfo = tokenInfo,
            UserInfo = userInfo
        };

    }

    public Task<GetAccessTokenResponse> GetAccessTokenWithRefreshToken(GetTokenWithRefreshTokenRequest request)
    {
        var refreshToken = request.RefreshToken;
        var accessToken = _authorizeRepository.GetAccessTokenWithRefreshToken(refreshTokenToAuthorize);
    }

    public Task<GetAccessTokenResponse> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest request)
    {
        var salt = ""; // todo: retrieve user-specific salt
        var userCredentials = new ProtectedUserCredentials(request.Username, request.Password, salt);
    }
}
