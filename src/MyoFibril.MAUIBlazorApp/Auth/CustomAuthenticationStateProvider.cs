using Microsoft.AspNetCore.Components.Authorization;
using MyoFibril.MAUIBlazorApp.Services.Local;
using System.Security.Claims;

namespace MyoFibril.MAUIBlazorApp.Auth;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IUserService _userService;
    public CustomAuthenticationStateProvider(IUserService userService)
    {
        _userService = userService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(new ClaimsPrincipal());
    }
}