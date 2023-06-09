namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class InvalidAuthorizeTokenRequestException : Exception
{
    public InvalidAuthorizeTokenRequestException() : base("Invalid authorize token request.")
    {
        
    }
}