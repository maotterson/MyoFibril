using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.Strava.Responses.OAuth;
public record NewAccessTokenResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = default!;

    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = default!;

    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; init; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; } = default!;
}