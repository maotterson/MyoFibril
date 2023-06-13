using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class GetTokenWithRefreshTokenRequest
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;
}