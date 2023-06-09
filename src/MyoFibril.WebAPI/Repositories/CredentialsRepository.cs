using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;

namespace MyoFibril.WebAPI.Repositories;

public class CredentialsRepository : ICredentialsRepository
{
    private const string COLLECTION_NAME = "credentials";

    public CredentialsRepository()
    {
        
    }

    public async Task<UserCredentialsEntity> GetCredentialsForUsername(string username)
    {

    }
}
