namespace MyoFibril.Contracts.WebAPI.Auth.Models;
public class UserRegisterCredentials
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Email { get; set; } = default!;
}