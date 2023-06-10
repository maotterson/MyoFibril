namespace MyoFibril.Domain.Entities.Auth;
public class UserCredentialsEntity
{
    public string Username { get; set; } = default!;
    public string HashedPassword { get; set; } = default!;
    public string Salt { get; set; } = default!;
    public string Email { get; set; } = default!;
}