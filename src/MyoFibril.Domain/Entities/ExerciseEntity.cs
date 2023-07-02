using System.Text.Json.Serialization;

namespace MyoFibril.Domain.Entities;
public class ExerciseEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("muscleGroups")]
    public List<MuscleGroupEntity> MuscleGroups { get; set; } = default!;
}