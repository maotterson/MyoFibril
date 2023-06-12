using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Services.Interfaces;
using MyoFibril.WebAPI.Utils.Jwt;
using JWT.Builder;

namespace MyoFibril.WebAPI.Services;

public class JwtService : IJwtService
{
    private IJwtAlgorithm _algorithm;
    public JwtService(IConfiguration configuration)
    {
        var privateKey = configuration["Jwt:PrivateKeyPem"] ?? throw new NullReferenceException("Missing private key for JWT signing");
        var publicKey = configuration["Jwt:PublicKeyPem"] ?? throw new NullReferenceException("Missing public key for JWT signing"); ;
        var publicKeyProvider = JwtUtils.CreatePublicRSAProviderFromPem(publicKey);
        var privateKeyProvider = JwtUtils.CreatePrivateRSAProviderFromPem(privateKey);
        _algorithm = new RS256Algorithm(publicKeyProvider, privateKeyProvider);
    }
    public async Task<string> GetAccessTokenWithRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public async Task<(string accessToken, string refreshToken)> GetTokensWithCredentials(UserCredentialsEntity credentials)
    {
        var accessToken = JwtBuilder.Create()
                      .WithAlgorithm(_algorithm)
                      .AddClaim("expires-at", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim("username", credentials.Username)
                      .AddClaim("email", credentials.Email)
                      .Encode();
        var refreshToken = JwtBuilder.Create()
                      .WithAlgorithm(_algorithm)
                      .AddClaim("username", credentials.Username)
                      .AddClaim("email", credentials.Email)
                      .Encode(); // todo: add implementation to generate a refresh token

        return (accessToken, refreshToken);
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
