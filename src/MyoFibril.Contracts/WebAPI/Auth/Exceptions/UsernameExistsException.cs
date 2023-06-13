namespace MyoFibril.Contracts.WebAPI.Auth.Exceptions;
public class UsernameExistsException : Exception
{
    public UsernameExistsException() : base("Username already exists.")
    {

    }
}