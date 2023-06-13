namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class ErrorUpdatingRefreshTokenException : Exception
{
    public ErrorUpdatingRefreshTokenException() : base("Error updating refresh token.")
    {

    }
}