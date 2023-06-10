using MyoFibril.Domain.Entities.Auth;

namespace MyoFibril.WebAPI.Services.Interfaces;

public interface IJwtService
{
    Task<(string accessToken, string refreshToken)> GetTokensWithCredentials(UserCredentialsEntity credentials);
}
