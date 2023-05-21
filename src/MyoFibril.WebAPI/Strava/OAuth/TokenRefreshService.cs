using MyoFibril.Contracts.Strava.OAuth;
using MyoFibril.Contracts.Strava.Static;
using MyoFibril.WebAPI.Strava.OAuth.Interfaces;
using System.Text.Json;
using System.Web;

namespace MyoFibril.WebAPI.Strava.OAuth;

public class TokenRefreshService : ITokenRefreshService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    public TokenRefreshService(IHttpClientFactory factory, IConfiguration configuration)
    {
        _httpClientFactory = factory;
        _configuration = configuration;
    }

    public async Task<NewAccessTokenResponse> RefreshAccessToken()
    {
        using var httpClient = _httpClientFactory.CreateClient("strava-oauth");
        var uriBuilder = new UriBuilder(StravaEndpoints.API_BASE_URL! + StravaEndpoints.GET_ACCESS_TOKEN);

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["client_id"] = _configuration["StravaApp:ClientId"]!;
        query["client_secret"] = _configuration["StravaApp:ClientSecret"]!;
        query["refresh_token"] = _configuration["StravaApp:RefreshToken"]!;
        query["grant_type"] = "refresh_token";
        uriBuilder.Query = query.ToString();

        var response = await httpClient.PostAsync(uriBuilder.ToString(), null);
        response.EnsureSuccessStatusCode(); // Ensure the request was successful

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<NewAccessTokenResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Allows case-insensitive property matching
        });

        if (tokenResponse is null) throw new NullReferenceException(nameof(tokenResponse));

        return tokenResponse;
    }
}