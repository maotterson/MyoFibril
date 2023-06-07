using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.Contracts.WebAPI.CreateActivity;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace MyoFibril.MAUIBlazorApp.Auth;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        try
        {
            var userInfo = await SecureStorage.GetAsync("accounttoken");
            if (userInfo is not null)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "user") };
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task<bool> Login(string username, string password)
    {
        var userCredentials = new UserCredentials { Username = username, Password = password}; // todo: replace user credentials tuple with class
        var tokenResponse = await GetTokenWithUserCredentials(userCredentials);
        if(tokenResponse is not null)
        {
            await SecureStorage.SetAsync("access-token", tokenResponse.AccessToken);
            await SecureStorage.SetAsync("refresh-token", tokenResponse.RefreshToken);
            await SecureStorage.SetAsync("access-token-expiration", tokenResponse.ExpiresAt.ToString());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }
        return false;
    }

    public async Task Logout()
    {
        SecureStorage.Remove("access-token");
        SecureStorage.Remove("refresh-token");
        SecureStorage.Remove("refresh-token-expiration");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private async Task<GetTokenWithUserCredentialsResponse> GetTokenWithUserCredentials(UserCredentials userCredentials)
    {
        var http = _httpClientFactory.CreateClient();

        var requestBaseUri = _configuration["Settings:API:BaseUri"];
        var requestUriBuilder = new UriBuilder(requestBaseUri);
        requestUriBuilder.Path = "authorize";
        requestUriBuilder.Query = "grant_type=accesstoken";
        var requestUri = requestUriBuilder.Uri;

        var requestBody = new GetTokenWithUserCredentialsRequest
        {
            Username = userCredentials.Username,
            Password = userCredentials.Password
        };

         var response = await http.PostAsJsonAsync<GetTokenWithUserCredentialsRequest>(requestUri, requestBody);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var getTokenResponse = JsonSerializer.Deserialize<GetTokenWithUserCredentialsResponse>(responseBody);


        return getTokenResponse;
    }
}