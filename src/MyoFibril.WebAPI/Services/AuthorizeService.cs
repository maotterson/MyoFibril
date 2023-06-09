using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
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
        var isVerified = _jwtService.VerifyToken(tokenToAuthorize);

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

        var accessToken = _jwtService.GetAccessTokenWithRefreshToken(refreshTokenToAuthorize);
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
        var accessToken = _jwtService.GetAccessTokenWithRefreshToken(refreshTokenToAuthorize);
        if (accessToken is null)
        {
            throw new InvalidRefreshTokenException();
        }
        return new GetAccessTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public Task<GetAccessTokenResponse> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest request)
    {
        var credentials = _credentialsRepository.GetCredentialsByUsername(request.Username);
        var salt = credentials.salt;
        var userCredentials = new ProtectedUserCredentials(request.Username, request.Password, salt);
        var validCredentials = _jwtService.VerifyCredentials(credentials, userCredentials);

        if (!validCredentials) throw new InvalidCredentialsException();

        var newAccessToken = _jwtService.CreateAccessToken();
        var newRefreshToken = _jwtService.CreateRefreshToken();

        return new GetAccessTokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
