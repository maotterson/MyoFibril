using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class AuthorizeService : IAuthorizeService
{
    private readonly ICredentialsRepository _credentialsRepository;
    private readonly IJwtService _jwtService;

    public AuthorizeService(IAuthorizeRepository authorizeRepository, ICredentialsRepository credentialsRepository, IJwtService jwtService)
    {
        _credentialsRepository = credentialsRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthorizeTokenResponse> AuthorizeToken(AuthorizeTokenRequest authorizeTokenRequest)
    {
        var tokenToAuthorize = authorizeTokenRequest.AccessToken;
        var refreshTokenToAuthorize = authorizeTokenRequest.RefreshToken;

        if (string.IsNullOrEmpty(tokenToAuthorize) || string.IsNullOrEmpty(refreshTokenToAuthorize)) throw new InvalidAuthorizeTokenRequestException();

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
            Success = isVerified,
            TokenInfo = tokenInfo,
            UserInfo = userInfo
        };

    }

    public async Task<GetAccessTokenResponse> GetAccessTokenWithRefreshToken(GetTokenWithRefreshTokenRequest request)
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

    public async Task<GetAccessTokenResponse> GetAccessTokenWithUserCredentials(GetTokenWithUserCredentialsRequest request)
    {
        var credentials = await _credentialsRepository.GetCredentialsForUsername(request.Username);
        var salt = credentials.Salt;
        var userCredentials = new ProtectedUserCredentials(request.Username, request.Password, credentials.Username);
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
