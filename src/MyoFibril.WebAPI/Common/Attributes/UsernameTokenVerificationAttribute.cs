using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MyoFibril.WebAPI.Services.Interfaces;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using MyoFibril.WebAPI.Common.Utils.Request;

namespace MyoFibril.WebAPI.Common.Attributes;

public class UsernameTokenVerificationAttribute : ActionFilterAttribute
{

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>()!;
            string accessToken = context.HttpContext.Request.ExtractBearerToken();
            string providedUsername = context.HttpContext.Request.Query["username"]!;

            var isValidProvidedUsername = jwtService.VerifyTokenAgainstUsername(accessToken, providedUsername);
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