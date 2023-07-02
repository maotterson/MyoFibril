using MyoFibril.Domain.Models;
using System.Text.Json.Serialization;

namespace MyoFibril.Domain.Entities;
public class PerformedExerciseEntity
{
    [JsonPropertyName("exercise")]
    public ExerciseEntity Exercise { get; set; } = default!;

    [JsonPropertyName("sets")]
    public List<SetInfo> Sets { get; set; } = new List<SetInfo>();
}