namespace MyoFibril.Domain.Models;
public class SetInfo
{
    public WeightInfo? Weight { get; set; } // optional parameter for weight information
    public int? Reps { get; set; } // optional parameter for reps performed
    public string? AdditionalDetails { get; set; } // optional parameter for additional details
}