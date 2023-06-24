using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Models.Auth;

namespace MyoFibril.WebAPI.Services.Interfaces;

public interface IJwtService
{
    (string accessToken, string refreshToken) GetTokensWithCredentials(UserCredentialsEntity credentials);
    bool VerifyToken(string accessToken);
    Task<string> GetAccessTokenWithRefreshToken(string refreshToken);
    bool VerifyCredentials(UserCredentialsEntity storedCredentials, ProtectedUserCredentials credentialsToVerify);
    Task<UserCredentialsEntity> GetCredentialsFromAccessToken(string accessToken);
    bool VerifyTokenAgainstUsername(string accessToken, string username);
}
