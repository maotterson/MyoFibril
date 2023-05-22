using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.Strava.CreateActivity;
public class StravaCreateActivityRequest
{
    [JsonPropertyName("name")]
    [JsonRequired]
    public string Name { get; set; } = default!;

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("sport_type")]
    [JsonRequired]
    public string SportType { get; set; } = default!;

    [JsonPropertyName("start_date_local")]
    [JsonRequired]
    public string StartDateLocal { get; set; } = default!;

    [JsonPropertyName("elapsed_time")]
    [JsonRequired]
    public int ElapsedTime { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("distance")]
    public float? Distance { get; set; }

    [JsonPropertyName("trainer")]
    public int? Trainer { get; set; }

    [JsonPropertyName("commute")]
    public int? Commute { get; set; }
}