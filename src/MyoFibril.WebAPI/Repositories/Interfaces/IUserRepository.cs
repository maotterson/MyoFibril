using MyoFibril.Domain.Entities;

namespace MyoFibril.WebAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> GetUserByUsername(string username);
    Task CreateUser(UserEntity user);
}
