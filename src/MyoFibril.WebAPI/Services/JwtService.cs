using JWT.Algorithms;
using JWT.Serializers;
using JWT;
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
        IJwtAlgorithm algorithm = new RS256Algorithm(certificate);
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        var token = encoder.Encode(credentials);
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
