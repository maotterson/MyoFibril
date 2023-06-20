using Microsoft.AspNetCore.Http.Extensions;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;

namespace MyoFibril.WebAPI.Common.Utils.Request;

public static class RequestExtensions
{
    public static string ExtractBearerToken(this HttpRequest request)
    {
        var authHeader = request.Headers["Authorization"].ToString();
        var authHeaderString = authHeader.Split(" ");
        if (authHeaderString[0] != "Bearer") throw new InvalidAccessTokenException();
        var accessToken = authHeaderString[1];
        return accessToken;
    }

    public static string ExtractRouteParameter(this HttpRequest request)
    {
        var basePathString = request.GetDisplayUrl();
        var pathString = request.Path;
        var pathUri = new Uri(basePathString);
        var routeParameter = pathUri.Segments.LastOrDefault() ?? throw new Exception("Invalid route parameter");

        return routeParameter;
    }
}
