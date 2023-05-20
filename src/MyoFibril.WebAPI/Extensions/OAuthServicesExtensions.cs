using MyoFibril.WebAPI.OAuth.Interfaces;
using MyoFibril.WebAPI.OAuth;

namespace MyoFibril.WebAPI.Extensions;

public static class OAuthServicesExtensions
{
    public static void AddStravaOAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("strava-oauth", httpClient =>
        {
            httpClient.BaseAddress = new Uri(configuration["StravaApp:RequestUri"]!);
        });
        services.AddScoped<ITokenCache, MemoryTokenCache>();
        services.AddScoped<ITokenRefreshService, TokenRefreshService>();
        services.AddScoped<IOAuthService, OAuthService>();
        services.AddTransient<AuthHeaderHandler>();
    }
}
