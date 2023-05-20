using Microsoft.Extensions.Caching.Memory;
using MyoFibril.WebAPI.OAuth;

namespace MyoFibril.WebAPI.Tests.OAuth;

public class MemoryTokenCacheTests
{
    private const string AccessTokenKey = "AccessToken";
    [Fact]
    public void SetAccessToken_ShouldSetAccessTokenInMemoryCache()
    {
        // Arrange
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var tokenCache = new MemoryTokenCache(memoryCache);
        var accessToken = "test-access-token";

        // Act
        tokenCache.SetAccessToken(accessToken);
        var cachedAccessToken = memoryCache.Get<string>(AccessTokenKey);

        // Assert
        Assert.Equal(accessToken, cachedAccessToken);
    }

    [Fact]
    public void GetAccessToken_WithValidAccessToken_ShouldReturnAccessToken()
    {
        // Arrange
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var tokenCache = new MemoryTokenCache(memoryCache);
        var accessToken = "test-access-token";
        memoryCache.Set(AccessTokenKey, accessToken);

        // Act
        var result = tokenCache.GetAccessToken();

        // Assert
        Assert.Equal(accessToken, result);
    }

    [Fact]
    public void GetAccessToken_WithoutAccessToken_ShouldReturnEmptyString()
    {
        // Arrange
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var tokenCache = new MemoryTokenCache(memoryCache);

        // Act
        var result = tokenCache.GetAccessToken();

        // Assert
        Assert.Equal(string.Empty, result);
    }
}