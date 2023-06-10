using MongoDB.Driver;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;

namespace MyoFibril.WebAPI.Repositories;

public class CredentialsRepository : ICredentialsRepository
{
    private const string COLLECTION_NAME = "credentials";
    private readonly IMongoCollection<UserCredentialsEntity> _credentialsCollection;
    private readonly IConfiguration _configuration;

    public CredentialsRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        _configuration = configuration;
        _credentialsCollection = mongoClient.GetDatabase(_configuration["Database:DatabaseName"]).GetCollection<UserCredentialsEntity>(COLLECTION_NAME);

    }

    public async Task<bool> DoesUsernameExist(string username)
    {
        var usernameExists = await _credentialsCollection.Find(u => u.Username == username).CountDocumentsAsync() > 0;
        return usernameExists;
    }

    public async Task<UserCredentialsEntity> GetCredentialsForUsername(string username)
    {
        var credentialsQuery = await _credentialsCollection.Find(u => u.Username == username).ToListAsync();

        if (credentialsQuery.Count != 1 || credentialsQuery is null) throw new InvalidCredentialsException();
        var credentials = credentialsQuery.SingleOrDefault();

        return credentials!;
    }

    public async Task RegisterNewUserWithProtectedCredentials(ProtectedUserCredentials protectedUserCredentials)
    {
        var userCredentials = new UserCredentialsEntity
        {
            Username = protectedUserCredentials.Username,
            HashedPassword = protectedUserCredentials.HashedPassword,
            Salt = protectedUserCredentials.Salt,
        };
        await _credentialsCollection.InsertOneAsync(userCredentials);
    }
}
