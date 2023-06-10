using MyoFibril.Domain.Entities.Auth;

namespace MyoFibril.WebAPI.Repositories.Interfaces;

public interface ICredentialsRepository
{
    Task<UserCredentialsEntity> GetCredentialsForUsername(string username);
    Task<bool> DoesUsernameExist(string username);
}
