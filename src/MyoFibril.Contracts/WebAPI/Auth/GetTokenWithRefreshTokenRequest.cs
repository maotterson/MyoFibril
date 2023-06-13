using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class GetTokenWithRefreshTokenRequest : AbstractAuthorizeRequest
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;
}