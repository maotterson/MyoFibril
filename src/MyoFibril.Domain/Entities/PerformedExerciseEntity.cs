using MyoFibril.Domain.Models;

namespace MyoFibril.Domain.Entities;
public class PerformedExerciseEntity
{
    public ExerciseEntity Exercise { get; set; } = default!;
    public List<SetInfo> Sets { get; set; } = new List<SetInfo>();
}