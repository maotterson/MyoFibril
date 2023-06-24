using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class LogoutRequest
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;
}