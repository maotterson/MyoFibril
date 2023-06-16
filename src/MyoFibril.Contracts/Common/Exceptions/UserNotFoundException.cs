namespace MyoFibril.Contracts.Common.Exceptions;
public class UserNotFoundException : Exception
{
    public UserNotFoundException(string username) : base($"User {username} not found.")
    {  
    }

    public UserNotFoundException() : base($"User not found.")
    {
    }
}