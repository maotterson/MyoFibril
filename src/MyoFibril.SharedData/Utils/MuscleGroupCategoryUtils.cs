using MyoFibril.Domain.Entities;
using MyoFibril.Domain.Enums;

namespace MyoFibril.SharedData.Utils;

public static class MuscleGroupCategoryUtils
{
    public static MuscleGroupEntity Pectorals = new MuscleGroupEntity { Name = "Pectorals", Slug = "pectorals", MuscleGroupCategory = MuscleGroupCategory.Chest };
    public static MuscleGroupEntity Abdominals = new MuscleGroupEntity { Name = "Abdominals", Slug = "abdominals", MuscleGroupCategory = MuscleGroupCategory.Core };
    public static MuscleGroupEntity Obliques = new MuscleGroupEntity { Name = "Obliques", Slug = "obliques", MuscleGroupCategory = MuscleGroupCategory.Core };
    public static MuscleGroupEntity SpinalErectors = new MuscleGroupEntity { Name = "Spinal Erectors", Slug = "spinal-erectors", MuscleGroupCategory = MuscleGroupCategory.Back };
    public static MuscleGroupEntity Deltoids = new MuscleGroupEntity { Name = "Deltoids", Slug = "deltoids", MuscleGroupCategory = MuscleGroupCategory.Shoulders };
    public static MuscleGroupEntity Trapezius = new MuscleGroupEntity { Name = "Trapezius", Slug = "trapezius", MuscleGroupCategory = MuscleGroupCategory.Shoulders };
    public static MuscleGroupEntity Lats = new MuscleGroupEntity { Name = "Lats", Slug = "lats", MuscleGroupCategory = MuscleGroupCategory.Back };
    public static MuscleGroupEntity Biceps = new MuscleGroupEntity { Name = "Biceps", Slug = "biceps", MuscleGroupCategory = MuscleGroupCategory.Arms };
    public static MuscleGroupEntity Triceps = new MuscleGroupEntity { Name = "Triceps", Slug = "triceps", MuscleGroupCategory = MuscleGroupCategory.Arms };
    public static MuscleGroupEntity HipFlexors = new MuscleGroupEntity { Name = "Hip Flexors", Slug = "hip-flexors", MuscleGroupCategory = MuscleGroupCategory.Core };
    public static MuscleGroupEntity Calves = new MuscleGroupEntity { Name = "Calves", Slug = "calves", MuscleGroupCategory = MuscleGroupCategory.Legs };
    public static MuscleGroupEntity Quadriceps = new MuscleGroupEntity { Name = "Quadriceps", Slug = "quadriceps", MuscleGroupCategory = MuscleGroupCategory.Legs };
    public static MuscleGroupEntity Hamstrings = new MuscleGroupEntity { Name = "Hamstrings", Slug = "hamstrings", MuscleGroupCategory = MuscleGroupCategory.Legs };
    public static MuscleGroupEntity Glutes = new MuscleGroupEntity { Name = "Glutes", Slug = "glutes", MuscleGroupCategory = MuscleGroupCategory.Legs };

    public static List<MuscleGroupEntity> MUSCLE_GROUP_LIST = new List<MuscleGroupEntity>
    {
        Pectorals, Abdominals, Obliques, SpinalErectors, Deltoids, Trapezius, Lats, Biceps, Triceps, HipFlexors, Calves, Quadriceps, Hamstrings, Glutes
    };

    public static List<MuscleGroupEntity> GetMuscleGroupsFromSlugs(List<string> slugs)
    {
        List<MuscleGroupEntity> matches = MUSCLE_GROUP_LIST
            .Where(mg => slugs.Contains(mg.Slug))
            .ToList();

        return matches;
    }
}
