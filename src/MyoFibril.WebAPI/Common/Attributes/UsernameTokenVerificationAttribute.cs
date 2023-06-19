using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MyoFibril.WebAPI.Services.Interfaces;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;

namespace MyoFibril.WebAPI.Common.Attributes;

public class UsernameTokenVerificationAttribute : ActionFilterAttribute
{
    private readonly IJwtService _jwtService;
    public UsernameTokenVerificationAttribute(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            string accessToken = context.HttpContext.Request.Headers["Authorization"]!;
            string providedUsername = context.HttpContext.Request.Query["username"]!;

            var isValidProvidedUsername = _jwtService.VerifyTokenAgainstUsername(accessToken, providedUsername);
            if (!isValidProvidedUsername) throw new UsernameTokenMismatchException();
            base.OnActionExecuting(context);
        }
        catch
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}