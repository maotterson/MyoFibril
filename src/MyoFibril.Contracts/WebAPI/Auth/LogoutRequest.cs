using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class LogoutRequest
{
    [JsonPropertyName("login_id")]
    public Guid LoginId { get; set; }
}