using MyoFibril.Contracts.Common.Exceptions;
using MyoFibril.Domain.Entities;
using MyoFibril.WebAPI.Repositories.Interfaces;

namespace MyoFibril.WebAPI.Repositories.Memory;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<UserEntity> _users = new List<UserEntity>()
    {
        new UserEntity
        {
            Id = "123456789",
            Username = "Testuser",
            Email = "Test@email.com",
            StravaAccount = null
        }
    };

    public async Task CreateUser(UserEntity user)
    {
        _users.Add(user);
    }

    public async Task<UserEntity> GetUserByUsername(string username)
    {
        var user = _users.FirstOrDefault(a => a.Username == username);
        if (user is null) throw new UserNotFoundException(username); // todo more specific exception

        return user;
    }
}
