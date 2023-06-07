using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace MyoFibril.MAUIBlazorApp.Auth;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
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
        var token = await GetTokenWithUserCredentials(userCredentials);
        if(token is not null)
        {
            await SecureStorage.SetAsync("accounttoken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }
        return false;
    }

    public async Task Logout()
    {
        SecureStorage.Remove("accounttoken");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private async Task<string> GetTokenWithUserCredentials(UserCredentials userCredentials)
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


        // todo: user credentials flow implementation
        return "validtoken";
    }
}