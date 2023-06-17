﻿using MyoFibril.Contracts.WebAPI.Auth.Exceptions;

namespace MyoFibril.WebAPI.Utils.Request;

public static class RequestExtensions
{
    public static string ExtractBearerToken(this HttpRequest request)
    {
        var authHeader = request.Headers["Authentication"].ToString();
        var authHeaderString = authHeader.Split(" ");
        if (authHeaderString[0] != "Bearer") throw new InvalidAccessTokenException();
        var accessToken = authHeaderString[1];
        return accessToken;
    }
}