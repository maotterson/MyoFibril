using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class GetAccessTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;
    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; init; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
}