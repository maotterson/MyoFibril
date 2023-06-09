using MyoFibril.Contracts.WebAPI.Auth.Models;
using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class AuthorizeTokenResponse
{
    [JsonPropertyName("is_authorized")]
    public bool IsAuthorized { get; set; } = default!;
    [JsonPropertyName("user_info")]
    public UserInfo? UserInfo { get; set; } = default!;
    [JsonPropertyName("token_info")]
    public GetAccessTokenResponse? TokenInfo { get; set; } = default!;
}