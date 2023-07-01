using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Common.Attributes;

public class LogRequestAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var callingController = context.Controller;
        var requestMethod = context.HttpContext.Request.Method;
        var requestPath = context.HttpContext.Request.Path;

        //logger.LogInformation($"{requestMethod}:{requestPath}");
    }
}
