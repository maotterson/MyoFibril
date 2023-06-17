namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class UsernameTokenMismatchException : Exception
{
    public UsernameTokenMismatchException() : base("Username not valid for the provided token.")
    {
            
    }
}