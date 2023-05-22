namespace MyoFibril.Contracts.Strava.Models;
public class StravaFault
{
    public StravaError[]? Errors { get; set; }
    public string? Message { get; set; }
}