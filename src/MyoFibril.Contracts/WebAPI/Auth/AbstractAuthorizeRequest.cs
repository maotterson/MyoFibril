using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class AbstractAuthorizeRequest
{
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = default!;
}