using System.Text.Json.Serialization;

namespace MyoFibril.Domain.Models;
public class SetInfo
{
    [JsonPropertyName("weight")]
    public WeightInfo? Weight { get; set; } // optional parameter for weight information

    [JsonPropertyName("reps")]
    public int? Reps { get; set; } // optional parameter for reps performed

    [JsonPropertyName("additionalDetails")]
    public string? AdditionalDetails { get; set; } // optional parameter for additional details
}