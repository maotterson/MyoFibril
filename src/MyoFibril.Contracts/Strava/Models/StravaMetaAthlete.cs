using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.Strava.Models;
public class StravaMetaAthlete
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public long Id { get; set; }
}