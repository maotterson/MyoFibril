using MongoDB.Driver;
using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;

namespace MyoFibril.WebAPI.Repositories;

public class CredentialsRepository : ICredentialsRepository
{
    private const string COLLECTION_NAME = "credentials";
    // todo implement injectable mongodb service

    public CredentialsRepository()
    {
        
    }

    public async Task<UserCredentialsEntity> GetCredentialsForUsername(string username)
    {
        var collection = _mongoService.GetCollection(COLLECTION_NAME);
    }
}
