using BCrypt.Net;

namespace MyoFibril.WebAPI.Models.Auth;

public class ProtectedUserCredentials
{
    private readonly string _username;
    private readonly string _hashedPassword;
    private readonly string _salt;
    private readonly string _email;
    public ProtectedUserCredentials(string username, string password, string salt, string email)
    {
        _username = username;
        _salt = salt;
        _email = email;
        _hashedPassword = HashPassword(password, salt); // todo use encryption/hashing algorithm

    }

    private string HashPassword(string password, string salt)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }
}