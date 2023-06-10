using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Models.Auth;

namespace MyoFibril.WebAPI.Repositories.Interfaces;

public interface ICredentialsRepository
{
    Task<UserCredentialsEntity> GetCredentialsForUsername(string username);
    Task<bool> DoesUsernameExist(string username);
    Task<UserCredentialsEntity> RegisterNewUserWithProtectedCredentials(ProtectedUserCredentials protectedUserCredentials);
}
