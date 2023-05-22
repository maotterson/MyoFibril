using MyoFibril.Contracts.Strava.CreateActivity;
using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.CreateActivity;
public class CreateActivityResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("strava_activity")]
    public StravaCreateActivityResponse? StravaActivity { get; set; }
}