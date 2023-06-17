using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using MyoFibril.Domain.Entities;

namespace MyoFibril.Contracts.WebAPI.GetActivity;
public class GetActivityResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("strava_activity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StravaDetailedActivity? StravaActivity { get; set; }

    [JsonPropertyName("performed_exercises")]
    public List<PerformedExerciseEntity>? PerformedExercises { get; set; }
}