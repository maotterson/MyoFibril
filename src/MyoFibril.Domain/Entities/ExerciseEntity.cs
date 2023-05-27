namespace MyoFibril.Domain.Entities;
public class ExerciseEntity
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<MuscleGroupEntity> MuscleGroups { get; set; } = default!;
}