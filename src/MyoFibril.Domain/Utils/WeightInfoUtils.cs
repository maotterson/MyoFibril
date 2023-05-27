using MyoFibril.Domain.Entities;
using MyoFibril.Domain.Enums;
using MyoFibril.Domain.Models;

namespace MyoFibril.Domain.Utils;
public static class WeightInfoUtils
{
    public static void ConvertToMetricUnits(this PerformedExerciseEntity performedExercise)
    {
        foreach(var set in performedExercise.Sets)
        {
            set.ConvertTo(WeightInfoUnit.Kilograms);
        }
    }
    public static void ConvertToImperialUnits(this PerformedExerciseEntity performedExercise)
    {
        foreach (var set in performedExercise.Sets)
        {
            set.ConvertTo(WeightInfoUnit.Pounds);
        }
    }

    private static void ConvertTo(this SetInfo set, WeightInfoUnit unit)
    {
        if (set.Weight!.Units == unit) return;

        switch (unit)
        {
            case WeightInfoUnit.Kilograms:
                set.Weight!.Value /= 2.2;
                break;
            case WeightInfoUnit.Pounds:
                set.Weight!.Value *= 2.2;
                break;
            default:
                throw new Exception($"Unknown {typeof(WeightInfoUnit)}: {unit}");
        }

        set.Weight!.Value 
        set.Weight!.Units = unit;
    }
}