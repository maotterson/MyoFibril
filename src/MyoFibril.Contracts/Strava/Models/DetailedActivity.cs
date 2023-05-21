using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.Strava.Models;
public class DetailedActivity
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("athlete")]
    public MetaAthlete? Athlete { get; set; }
    [JsonPropertyName("distance")]
    public float? Distance { get; set; }
    [JsonPropertyName("moving_time")]
    public int? MovingTime { get; set; }
    [JsonPropertyName("elapsed_time")]
    public int? ElapsedTime { get; set; }
    [JsonPropertyName("sport_type")]
    public SportType? SportType { get; set; }
    [JsonPropertyName("start_date")]
    public DateTime? StartDate { get; set; }
    [JsonPropertyName("start_date_local")]
    public DateTime? StartDateLocal { get; set; }
    [JsonPropertyName("timezone")]
    public string? TimeZone { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("workout_type")]
    public string? WorkoutType { get; set; }


}