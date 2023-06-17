namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class InvalidAccessTokenException : Exception
{
    public InvalidAccessTokenException() : base("Invalid authorize token request.")
    {
        
    }
}