using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.MAUIBlazorApp.Services.Local;
using MyoFibril.MAUIBlazorApp.Storage.Models;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace MyoFibril.MAUIBlazorApp.Auth;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IStorageService _storageService;
    public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration, IStorageService storageService)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _storageService = storageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        var emptyAuthState = new AuthenticationState(new ClaimsPrincipal(identity));
        try
        {
            var tokenInfo = await _storageService.GetItemAsync<TokenInfo>("token_info");

            // check if a token exists
            if (tokenInfo is null) return emptyAuthState;

            // check validity of token
            var isValidToken = await VerifyToken(tokenInfo);
            if (!isValidToken)
            {
                // todo: remove invalidated token first
                await Logout();
                return emptyAuthState;
            }


            var claims = new[] { 
                new Claim("access_token", tokenInfo.AccessToken),
                new Claim("refresh_token", tokenInfo.RefreshToken),
            };
            identity = new ClaimsIdentity(claims, "Server authentication");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
            return emptyAuthState;
        }
    }

    public async Task<RegisterNewUserResponse> CreateAccountWithCredentials(UserRegisterCredentials credentials)
    {
        var http = _httpClientFactory.CreateClient();

        var requestBaseUri = _configuration["Settings:API:BaseUri"];
        var requestUriBuilder = new UriBuilder(requestBaseUri);
        requestUriBuilder.Path = "Register";
        var requestUri = requestUriBuilder.Uri;

        var requestBody = new RegisterNewUserRequest
        {
            Username = credentials.Username,
            Password = credentials.Password,
            Email = credentials.Email
        };

        var response = await http.PostAsJsonAsync<RegisterNewUserRequest>(requestUri, requestBody);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var registerNewUserResponse = JsonSerializer.Deserialize<RegisterNewUserResponse>(responseBody);

        // store tokens
        var tokenInfo = new TokenInfo
        {
            AccessToken = registerNewUserResponse.TokenInfo.AccessToken,
            RefreshToken = registerNewUserResponse.TokenInfo.RefreshToken,
            ExpiresAt = registerNewUserResponse.TokenInfo.ExpiresAt
        };
        await _storageService.StoreItemAsync<TokenInfo>("token_info", tokenInfo);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        return registerNewUserResponse;
    }

    private async Task<bool> VerifyToken(TokenInfo tokenInfo)
    {
        // todo: implement verification check for token
        return true;
    }


    public async Task<bool> Login(string username, string password)
    {
        try
        {
            var userCredentials = new UserLoginCredentials { Username = username, Password = password };
            var tokenResponse = await GetTokenWithUserCredentials(userCredentials);
            if (tokenResponse is not null)
            {
                var tokenInfo = new TokenInfo
                {
                    AccessToken = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                    ExpiresAt = tokenResponse.ExpiresAt
                };

                await _storageService.StoreItemAsync("token_info", tokenInfo);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return true;
            }
            return false;
        }
        catch
        {
            return false; // todo: could add some logging etc
        }
    }

    public async Task Logout()
    {
        try
        {
            // send refresh token for revoking
            var tokens = await _storageService.GetItemAsync<TokenInfo>("token_info");
            var refreshTokenToRevoke = tokens.RefreshToken;

            var http = _httpClientFactory.CreateClient();

            var requestBaseUri = _configuration["Settings:API:BaseUri"];
            var requestUriBuilder = new UriBuilder(requestBaseUri);
            requestUriBuilder.Path = "Authorize/Logout";
            var requestUri = requestUriBuilder.Uri;

            var logoutRequestBody = new LogoutRequest
            {
                RefreshToken = refreshTokenToRevoke
            };

            var response = await http.PostAsJsonAsync<LogoutRequest>(requestUri, logoutRequestBody); 
        }
        catch(Exception ex)
        {
            // todo could handle an error status code
        }
        finally
        {
            _storageService.RemoveItem("token_info");
            _storageService.RemoveItem("user_info");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    private async Task<GetAccessTokenResponse> GetTokenWithRefreshToken(string refreshToken)
    {
        var http = _httpClientFactory.CreateClient();

        var requestBaseUri = _configuration["Settings:API:BaseUri"];
        var requestUriBuilder = new UriBuilder(requestBaseUri);
        requestUriBuilder.Path = "Authorize/Refresh";
        var requestUri = requestUriBuilder.Uri;

        var requestBody = new GetTokenWithRefreshTokenRequest
        {
            RefreshToken = refreshToken,
        };

        var response = await http.PostAsJsonAsync<GetTokenWithRefreshTokenRequest>(requestUri, requestBody);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var getAccessTokenResponse = JsonSerializer.Deserialize<GetAccessTokenResponse>(responseBody);

        return getAccessTokenResponse;
    }


    private async Task<GetAccessTokenResponse> GetTokenWithUserCredentials(UserLoginCredentials userCredentials)
    {
        var http = _httpClientFactory.CreateClient();

        var requestBaseUri = _configuration["Settings:API:BaseUri"];
        var requestUriBuilder = new UriBuilder(requestBaseUri);
        requestUriBuilder.Path = "Authorize/Credentials";
        var requestUri = requestUriBuilder.Uri;

        var requestBody = new GetTokenWithUserCredentialsRequest
        {
            Username = userCredentials.Username,
            Password = userCredentials.Password
        };

        var response = await http.PostAsJsonAsync<GetTokenWithUserCredentialsRequest>(requestUri, requestBody);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var getTokenResponse = JsonSerializer.Deserialize<GetAccessTokenResponse>(responseBody);

        return getTokenResponse;
    }
}