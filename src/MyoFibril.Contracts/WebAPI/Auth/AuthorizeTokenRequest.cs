using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class AuthorizeTokenRequest
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;
}