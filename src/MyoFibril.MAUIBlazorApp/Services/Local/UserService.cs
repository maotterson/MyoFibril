using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.MAUIBlazorApp.Auth;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public class UserService : IUserService
{
    private readonly CustomAuthenticationStateProvider _authenticationProvider;
    public UserService(CustomAuthenticationStateProvider authenticationProvider)
    {
        _authenticationProvider = authenticationProvider;
        
    }

    public async Task<bool> CheckLoginCredentials(string username, string password)
    {
        var isSuccessfulLogin = await _authenticationProvider.Login(username, password);
        return isSuccessfulLogin;
    }

    public async Task Logout()
    {
        await _authenticationProvider.Logout();
    }
}