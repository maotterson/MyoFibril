using BCrypt.Net;

namespace MyoFibril.WebAPI.Models.Auth;

public class ProtectedUserCredentials
{
    private readonly string _username;
    private readonly string _protectedPassword;
    public ProtectedUserCredentials(string username, string password, string salt)
    {
        _username = username;
        _protectedPassword = HashPassword(password, salt); // todo use encryption/hashing algorithm
    }

    private string HashPassword(string password, string salt)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }
}