namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class ErrorRevokingRefreshTokenException : Exception
{
    public ErrorRevokingRefreshTokenException() : base("Error revoking refresh token.")
    {

    }
}