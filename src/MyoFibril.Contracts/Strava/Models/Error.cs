using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.Strava.Models;
public class Error
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("field")]
    public string? Field { get; set; }
    [JsonPropertyName("resource")]
    public string? Resource { get; set; }
}