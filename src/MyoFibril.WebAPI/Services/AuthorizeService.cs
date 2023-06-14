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

    public AuthorizeService(ICredentialsRepository credentialsRepository, IJwtService jwtService)
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
        var isVerified = await _jwtService.VerifyToken(tokenToAuthorize);

        if (isVerified)
        {
            var credentials = await _jwtService.GetCredentialsFromAccessToken(tokenToAuthorize);
            tokenInfo = new GetAccessTokenResponse
            {
                AccessToken = tokenToAuthorize,
                RefreshToken = refreshTokenToAuthorize
            };
            userInfo = new UserInfo
            {
                Username = credentials.Username,
                Email = credentials.Email
            };
        }

        var accessToken = await _jwtService.GetAccessTokenWithRefreshToken(refreshTokenToAuthorize);
        if(accessToken is not null)
        {
            isVerified = true;
            var credentials = await _jwtService.GetCredentialsFromAccessToken(tokenToAuthorize);
            tokenInfo = new GetAccessTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenToAuthorize
            };
            userInfo = new UserInfo
            {
                Username = credentials.Username,
                Email = credentials.Email
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
        var accessToken = await _jwtService.GetAccessTokenWithRefreshToken(refreshToken);
        if (accessToken is null) throw new InvalidRefreshTokenException();

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
        var userCredentials = new ProtectedUserCredentials(request.Username, request.Password, salt);
        var validCredentials = await _jwtService.VerifyCredentials(credentials, userCredentials);

        if (!validCredentials) throw new InvalidCredentialsException();

        // grab the rest of the relevant user information for the verified user
        var credentialsEntity = await _credentialsRepository.GetCredentialsForUsername(request.Username);

        var (accessToken, refreshToken) = await _jwtService.GetTokensWithCredentials(credentialsEntity);

        // save currently issued refresh token alongside credentials
        await _credentialsRepository.UpdateRefreshTokenForUsername(credentialsEntity.Username, refreshToken);

        return new GetAccessTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            // todo expiry information
        };
    }

    public async Task<LogoutResponse> Logout(LogoutRequest logoutRequest)
    {
        try
        {
            var refreshTokenToRevoke = logoutRequest.RefreshToken;
            await _credentialsRepository.RevokeStoredRefreshToken(refreshTokenToRevoke);
            return new LogoutResponse
            {
                Success = true
            };
        }
        catch
        {
            return new LogoutResponse
            {
                Success = false
            };
        }
    }
}
