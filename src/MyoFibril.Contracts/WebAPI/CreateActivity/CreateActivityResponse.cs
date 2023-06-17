using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Domain.Entities;
using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.CreateActivity;
public class CreateActivityResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("strava_activity")]
    public StravaCreateActivityResponse? StravaActivity { get; set; }
    [JsonPropertyName("performed_exercises")]
    public List<PerformedExerciseEntity>? PerformedExercises { get; set; }
}