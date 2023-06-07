using System.Text.Json.Serialization;

namespace MyoFibril.MAUIBlazorApp.Storage.Models;
public class TokenInfo
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; set; }
}