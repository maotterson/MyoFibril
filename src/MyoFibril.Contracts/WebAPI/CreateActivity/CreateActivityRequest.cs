using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.CreateActivity;
public class CreateActivityRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

}