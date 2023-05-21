using MyoFibril.WebAPI.OAuth.Interfaces;
using MyoFibril.WebAPI.OAuth;
using MyoFibril.Contracts.Strava.Static;

namespace MyoFibril.WebAPI.Extensions;

public static class OAuthServicesExtensions
{
    public static void AddStravaOAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("strava-oauth", httpClient =>
        {
            httpClient.BaseAddress = new Uri(StravaEndpoints.API_BASE_URL);
        });
        services.AddScoped<ITokenCache, MemoryTokenCache>();
        services.AddScoped<ITokenRefreshService, TokenRefreshService>();
        services.AddScoped<IOAuthService, OAuthService>();
        services.AddTransient<AuthHeaderHandler>();
    }
}
