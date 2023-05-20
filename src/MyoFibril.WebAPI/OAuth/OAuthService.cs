namespace MyoFibril.WebAPI.OAuth;

public class OAuthService : IOAuthService
{
    private readonly ITokenRefreshService _tokenRefreshService;
    private readonly ITokenCache _tokenCache;

    public OAuthService(ITokenRefreshService tokenRefreshService, ITokenCache tokenCache)
    {
        _tokenRefreshService = tokenRefreshService;
        _tokenCache = tokenCache;
    }

    public async Task<string> GetAccessToken()
    {
        string accessToken = _tokenCache.GetAccessToken();

        if (string.IsNullOrEmpty(accessToken))
        {
            var tokenResponse = await _tokenRefreshService.RefreshAccessToken();
            accessToken = tokenResponse.AccessToken;
            _tokenCache.SetAccessToken(accessToken);
        }

        return accessToken;
    }
}
