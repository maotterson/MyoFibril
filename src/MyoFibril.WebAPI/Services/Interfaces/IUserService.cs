using MyoFibril.Contracts.WebAPI.GetUser;

namespace MyoFibril.WebAPI.Services.Interfaces;

public interface IUserService
{
    Task<GetUserResponse> GetUserByUsername(string username);
}
