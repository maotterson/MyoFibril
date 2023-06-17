namespace MyoFibril.Contracts.Common.Exceptions;
public class ActivityNotFoundException : Exception
{
    public ActivityNotFoundException() : base("Activity not found.")
    {
        
    }
}