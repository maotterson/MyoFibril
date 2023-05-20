namespace MyoFibril.Contracts.Common.Exceptions;
public class AccessTokenNotAvailableException : Exception
{
    public AccessTokenNotAvailableException() : base("Access token not available.")
    {

    }
}
