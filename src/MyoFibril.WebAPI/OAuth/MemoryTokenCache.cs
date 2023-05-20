﻿using Microsoft.Extensions.Caching.Memory;

namespace MyoFibril.WebAPI.OAuth;

public class MemoryTokenCache : ITokenCache
{
    private const string AccessTokenKey = "AccessToken";
    private readonly IMemoryCache _memoryCache;

    public MemoryTokenCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void SetAccessToken(string accessToken)
    {
        _memoryCache.Set(AccessTokenKey, accessToken);
    }

    public string GetAccessToken()
    {
        string accessToken = _memoryCache.Get<string>(AccessTokenKey) ?? string.Empty;
        return accessToken;
    }
}