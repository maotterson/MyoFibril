using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth.Models;
public class UserInfo
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = default!;
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;
}