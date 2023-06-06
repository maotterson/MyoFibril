using Castle.DynamicProxy;
using Microsoft.AspNetCore.Components;
using MyoFibril.MAUIBlazorApp.Services.Local;

namespace MyoFibril.MAUIBlazorApp.Auth;
public class AuthenticationInterceptor : IInterceptor
{
    private readonly NavigationManager _navigationManager;
    private readonly IUserService _userService;
    

    public AuthenticationInterceptor(NavigationManager navigationManager, IUserService userService)
    {
        _navigationManager = navigationManager;
        _userService = userService;
    }

    public void Intercept(IInvocation invocation)
    {
        if (invocation.Method.Name == "SetParametersAsync" && invocation.Arguments.Length > 0)
        {
            var target = invocation.Arguments[0];
            if (target != null && target.GetType().IsDefined(typeof(AuthorizeAttribute), inherit: true))
            {
                if (!IsUserLoggedIn())
                {
                    _navigationManager.NavigateTo("/Login");
                    return;
                }
            }
        }

        invocation.Proceed();
    }

    private bool IsUserLoggedIn()
    {
        return _userService.IsLoggedIn();
    }
}