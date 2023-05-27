using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MyoFibril.Domain.Enums;

namespace MyoFibril.Domain.Entities;
public class MuscleGroupEntity
{
    public Guid Guid { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public MuscleGroupCategory MuscleGroupCategory { get; set; } = default!;

}
