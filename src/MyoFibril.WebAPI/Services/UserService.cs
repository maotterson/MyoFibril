using MyoFibril.Contracts.WebAPI.GetUser;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService()
    {
            
    }
    public Task<GetUserResponse> GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }
}
