using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Models.Auth;

namespace MyoFibril.WebAPI.Services.Interfaces;

public interface IJwtService
{
    Task<(string accessToken, string refreshToken)> GetTokensWithCredentials(UserCredentialsEntity credentials);
    Task<bool> VerifyToken(string accessToken);
    Task<string> GetAccessTokenWithRefreshToken(string refreshToken);
    Task<bool> VerifyCredentials(UserCredentialsEntity storedCredentials, ProtectedUserCredentials credentialsToVerify);
}
