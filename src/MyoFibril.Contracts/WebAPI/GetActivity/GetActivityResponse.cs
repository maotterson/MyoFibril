using MyoFibril.Contracts.Strava.CreateActivity;
using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.GetActivity;
public class GetActivityResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("strava_activity")]
    public StravaCreateActivityResponse? StravaActivity { get; set; }
}