namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException() : base("Invalid refresh token.")
    {
        
    }
}