using MyoFibril.Contracts.Strava.Static;
using MyoFibril.WebAPI.Strava.OAuth.Interfaces;
using MyoFibril.WebAPI.Strava.OAuth;
using MyoFibril.WebAPI.Strava.Services;
using MyoFibril.WebAPI.Strava.Services.Interfaces;
using MyoFibril.WebAPI.Strava.Repositories.Interfaces;
using MyoFibril.WebAPI.Strava.Repositories;

namespace MyoFibril.WebAPI.Strava.Extensions;

public static class ApiServicesExtensions
{
    public static void AddStravaServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IStravaActivityService, StravaActivityService>();
        services.AddScoped<IStravaActivityCache, StravaActivityCache>();
    }
}
