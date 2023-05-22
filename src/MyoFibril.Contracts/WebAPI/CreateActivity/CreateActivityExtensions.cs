using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.Models;

namespace MyoFibril.Contracts.WebAPI.CreateActivity;
public static class CreateActivityExtensions
{
    public static StravaCreateActivityRequest AsStravaCreateActivityRequest(this CreateActivityRequest request)
    {
        // todo replace sample integration with more complete implementation
        var stravaRequest = new StravaCreateActivityRequest
        {
            Name = request.Name,
            Description = "Created by web api",
            SportType = StravaSportType.Workout.ToString(),
            StartDateLocal = DateTimeOffset.Now.ToLocalTime().ToString("o"),
            ElapsedTime = 333
        };
        return stravaRequest;
    }
}