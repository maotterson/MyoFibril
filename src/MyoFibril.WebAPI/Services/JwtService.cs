using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class JwtService : IJwtService
{
    public Task<string> GetAccessTokenWithRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<(string accessToken, string refreshToken)> GetTokensWithCredentials(UserCredentialsEntity credentials)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyCredentials(UserCredentialsEntity storedCredentials, ProtectedUserCredentials credentialsToVerify)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyToken(string accessToken)
    {
        throw new NotImplementedException();
    }
}
