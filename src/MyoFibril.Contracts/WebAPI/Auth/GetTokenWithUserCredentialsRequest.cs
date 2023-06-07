using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class GetTokenWithUserCredentialsRequest
{
    [JsonRequired]
    [JsonPropertyName("username")]
    public string Username { get; set; } = default!;
    [JsonRequired]
    [JsonPropertyName("password")]
    public string Password { get; set;} = default!;
}