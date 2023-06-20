using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Services.Interfaces;
using JWT.Builder;
using JWT.Exceptions;
using Amazon.SecurityToken.Model;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using Newtonsoft.Json.Linq;
using MongoDB.Driver;
using MyoFibril.WebAPI.Common.Utils.Jwt;
using System.Security.Cryptography;

namespace MyoFibril.WebAPI.Services;

public class JwtService : IJwtService
{
    private IJwtAlgorithm _algorithm;
    private ICredentialsRepository _credentialsRepository;
    private RSACryptoServiceProvider _publicKeyProvider;
    public JwtService(IConfiguration configuration, ICredentialsRepository credentialsRepository)
    {
        var privateKey = configuration["Jwt:PrivateKeyPem"] ?? throw new NullReferenceException("Missing private key for JWT signing");
        var publicKey = configuration["Jwt:PublicKeyPem"] ?? throw new NullReferenceException("Missing public key for JWT signing"); ;
        _publicKeyProvider = JwtUtils.CreatePublicRSAProviderFromPem(publicKey);
        var privateKeyProvider = JwtUtils.CreatePrivateRSAProviderFromPem(privateKey);
        _algorithm = new RS256Algorithm(_publicKeyProvider, privateKeyProvider);
        _credentialsRepository = credentialsRepository;
    }
    public RSACryptoServiceProvider GetPublicKeyProvider()
    {
        return _publicKeyProvider;
    }
    public async Task<string> GetAccessTokenWithRefreshToken(string refreshToken)
    {
        // decode refresh token
        var provider = new UtcDateTimeProvider();
        var serializer = new JsonNetSerializer();
        var validator = new JwtValidator(serializer, provider);
        var urlEncoder = new JwtBase64UrlEncoder();
        var decoder = new JwtDecoder(serializer, validator, urlEncoder, _algorithm);
        var json = decoder.DecodeToObject<IDictionary<string, object>>(refreshToken);

        // extract username from refresh token
        var username = json["username"].ToString() ?? throw new InvalidRefreshTokenException();

        // retrieve credentials required to generate access token
        var credentials = await _credentialsRepository.GetCredentialsForUsername(username);

        // compare stored refresh token with supplied refresh token
        if (credentials.RefreshToken != refreshToken) throw new InvalidRefreshTokenException();

        // generate accesstoken
        var accessToken = JwtBuilder.Create()
                     .WithAlgorithm(_algorithm)
                     .AddClaim("expires-at", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                     .AddClaim("username", credentials.Username)
                     .AddClaim("email", credentials.Email)
                     .Encode();

        return accessToken;
    }

    public async Task<UserCredentialsEntity> GetCredentialsFromAccessToken(string accessToken)
    {
        // decode access token
        var provider = new UtcDateTimeProvider();
        var serializer = new JsonNetSerializer();
        var validator = new JwtValidator(serializer, provider);
        var urlEncoder = new JwtBase64UrlEncoder();
        var decoder = new JwtDecoder(serializer, validator, urlEncoder, _algorithm);
        var json = decoder.DecodeToObject<IDictionary<string, object>>(accessToken);

        // extract username from token
        var username = json["username"].ToString() ?? throw new InvalidRefreshTokenException();

        // retrieve user credentials from repository
        var credentials = await _credentialsRepository.GetCredentialsForUsername(username);

        return credentials;
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

    public async Task<bool> VerifyCredentials(UserCredentialsEntity storedCredentials, ProtectedUserCredentials credentialsToVerify)
    {
        if (storedCredentials.Username != credentialsToVerify.Username)
            return false;

        if (storedCredentials.HashedPassword != credentialsToVerify.HashedPassword)
            return false;

        return true;
    }

    public async Task<bool> VerifyToken(string token)
    {
        try
        {
            var provider = new UtcDateTimeProvider();
            var serializer = new JsonNetSerializer();
            var validator = new JwtValidator(serializer, provider);
            var urlEncoder = new JwtBase64UrlEncoder();
            var decoder = new JwtDecoder(serializer, validator, urlEncoder, _algorithm);

            var json = decoder.DecodeToObject<IDictionary<string, object>>(token);
            var expires = long.Parse(json["expires-at"].ToString()!);
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (now > expires)
            {
                return false;
            }
            return true;
        }
        catch (TokenNotYetValidException)
        {
            return false;
        }
        catch (TokenExpiredException)
        {
            return false;
        }
        catch (SignatureVerificationException)
        {
            return false;
        }

        
    }

    public bool VerifyTokenAgainstUsername(string accessToken, string username)
    {
        // decode username from accesstoken
        var provider = new UtcDateTimeProvider();
        var serializer = new JsonNetSerializer();
        var validator = new JwtValidator(serializer, provider);
        var urlEncoder = new JwtBase64UrlEncoder();
        var decoder = new JwtDecoder(serializer, validator, urlEncoder, _algorithm);

        var json = decoder.DecodeToObject<IDictionary<string, object>>(accessToken);

        // validate that a username exists in the token
        var extractedUsername = json["username"].ToString() ?? throw new InvalidAccessTokenException();
        if (string.IsNullOrEmpty(extractedUsername)) return false;

        // verify the token against the provided username
        if (extractedUsername != username) return false;

        return true;
    }
}
