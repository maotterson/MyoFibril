namespace MyoFibril.Domain.Entities;
public class ExerciseEntity
{
    public string Name { get; set; } = default!;
    public List<MuscleGroupEntity> MuscleGroups { get; set; } = default!;
}