using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.Models;
using System.Text;

namespace MyoFibril.Contracts.WebAPI.CreateActivity;
public static class CreateActivityExtensions
{
    public static StravaCreateActivityRequest AsStravaCreateActivityRequest(this CreateActivityRequest request)
    {
        // todo replace sample integration with more complete implementation
        var stravaRequest = new StravaCreateActivityRequest
        {
            Name = request.Name,
            Description = request.BuildStravaDescription(),
            SportType = StravaSportType.Workout.ToString(),
            StartDateLocal = request.DateCreated.ToLocalTime().ToString("o"),
            ElapsedTime = 3600
        };
        return stravaRequest;
    }

    private static string BuildStravaDescription(this CreateActivityRequest request)
    {
        StringBuilder sb = new StringBuilder();
        if (request.PerformedExercises is null) return sb.ToString();

        // Variations:

        // Squats
        // Squats: N/A
        // Squats: x15, x15
        // Squats: 225x15, 225x15
        // Squats: 225x15 (note), x10 (note), N/A (note)
        foreach (var ex in request.PerformedExercises)
        {
            sb.Append($"{ex.Exercise.Name})");
            if (ex.Sets.Count == 0)
            {
                sb.Append($"\n");
                continue;
            }

            // output each set detail
            foreach (var set in ex.Sets)
            {
                if (set.Reps is null || set.Reps == 0)
                {
                    sb.Append($": N/A");
                }
                else if (set.Weight is null)
                {
                    sb.Append($": x{set.Reps}");
                }
                else
                {
                    sb.Append($": {set.Weight}x{set.Reps}");
                }

                if(set.AdditionalDetails is not null && !string.IsNullOrEmpty(set.AdditionalDetails))
                {
                    sb.Append($" ({set.AdditionalDetails})");
                }

                sb.Append(", ");
            }

            // trim trailing space and comma and go to new line
            sb.Remove(sb.Length - 1, 2);
            sb.Append('\n');
        }
        return sb.ToString();
    }
}