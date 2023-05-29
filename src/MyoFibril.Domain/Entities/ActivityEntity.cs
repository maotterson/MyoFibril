namespace MyoFibril.Domain.Entities;

public class ActivityEntity
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset DateCreated { get; set; }
    public long? StravaActivityId { get; set; }
    public List<PerformedExerciseEntity>? PerformedExercises { get; set; }
}
