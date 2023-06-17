using MongoDB.Driver;
using MyoFibril.Contracts.Common.Exceptions;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.Domain.Entities;
using MyoFibril.Domain.Entities.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;

namespace MyoFibril.WebAPI.Repositories;

public class UserRepository : IUserRepository
{
    private const string COLLECTION_NAME = "users";
    private readonly IMongoCollection<UserEntity> _usersCollection;
    private readonly IConfiguration _configuration;

    public UserRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        _configuration = configuration;
        _usersCollection = mongoClient.GetDatabase(_configuration["Database:DatabaseName"]).GetCollection<UserEntity>(COLLECTION_NAME);

    }
    public async Task CreateUser(UserEntity user)
    {
        await _usersCollection.InsertOneAsync(user);
    }

    public async Task<UserEntity> GetUserByUsername(string username)
    {
        var usersQuery = await _usersCollection.Find(u => u.Username == username).ToListAsync();

        if (usersQuery is null || usersQuery.Count != 1) throw new UserNotFoundException(username);
        var user = usersQuery.SingleOrDefault();

        return user;
    }
}
