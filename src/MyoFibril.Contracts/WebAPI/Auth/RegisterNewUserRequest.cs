using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class RegisterNewUserRequest : AbstractAuthorizeRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = default!;
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;
}