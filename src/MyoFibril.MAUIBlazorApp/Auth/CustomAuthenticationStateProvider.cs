using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MyoFibril.MAUIBlazorApp.Auth;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
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
        var userCredentials = (Username: username, Password: password); // todo: replace user credentials tuple with class
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

    private async Task<string> GetTokenWithUserCredentials((string, string) userCredentials)
    {
        // todo: user credentials flow implementation
        return "validtoken";
    }
}