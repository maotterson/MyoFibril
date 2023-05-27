using System.Text.Json.Serialization;

namespace MyoFibril.SharedData.Dto;

public class LoadExerciseDto
{
    [JsonPropertyName("name")]
    public string ExerciseName { get; set; } = default!;
    [JsonPropertyName("muscle-groups")]
    public List<string> MuscleGroupSlugs { get; set; } = default!;
}
