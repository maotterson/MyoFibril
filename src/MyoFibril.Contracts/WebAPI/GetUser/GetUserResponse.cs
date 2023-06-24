using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.GetUser;
public class GetUserResponse
{
    [JsonPropertyName("name")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("strava_account")]
    public string? StravaAccount { get; set; } = string.Empty;


}