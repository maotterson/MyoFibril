namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class InvalidCredentialsException  : Exception
{
    public InvalidCredentialsException() : base("Invalid user credentials.")
    {
        
    }
}