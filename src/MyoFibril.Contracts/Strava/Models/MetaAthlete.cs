using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.Strava.Models;
public class MetaAthlete
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public long Id { get; set; }
}