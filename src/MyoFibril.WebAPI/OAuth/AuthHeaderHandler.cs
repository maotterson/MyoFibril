using System.Net.Http.Headers;

namespace MyoFibril.WebAPI.OAuth;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IOAuthService _oauthService;

    public AuthHeaderHandler(IOAuthService oauthService)
    {
        _oauthService = oauthService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _oauthService.GetAccessToken();

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}