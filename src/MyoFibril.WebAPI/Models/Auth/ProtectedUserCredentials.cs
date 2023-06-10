using BCrypt.Net;

namespace MyoFibril.WebAPI.Models.Auth;

public class ProtectedUserCredentials
{
    public string Username { get; init; }
    public string HashedPassword { get; init; }
    public string Salt { get; init; }
    public string? Email { get; init; }
    public ProtectedUserCredentials(string username, string password, string salt, string email)
    {
        Username = username;
        Salt = salt;
        Email = email;
        HashedPassword = HashPassword(password, salt);
    }
    public ProtectedUserCredentials(string username, string password, string salt)
    {
        Username = username;
        Salt = salt;
        HashedPassword = HashPassword(password, salt);
    }

    private string HashPassword(string password, string salt)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }
}