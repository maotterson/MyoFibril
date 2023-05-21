using MyoFibril.Contracts.Strava.Static;
using MyoFibril.WebAPI.Strava.OAuth.Interfaces;
using MyoFibril.WebAPI.Strava.OAuth;
using MyoFibril.WebAPI.Strava.Services;

namespace MyoFibril.WebAPI.Strava.Extensions;

public class ApiServicesExtensions
{
    public static void AddStravaServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IStravaActivityService, StravaActivityService>();
    }
}
