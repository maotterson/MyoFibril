namespace MyoFibril.Contracts.Strava.Models;
public class Fault
{
    public Error[]? Errors { get; set; }
    public string? Message { get; set; }
}