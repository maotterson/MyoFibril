using MyoFibril.Contracts.WebAPI.GetUser;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<GetUserResponse> GetUserByUsername(string username)
    {
        var userEntity = await _userRepository.GetUserByUsername(username);

    }
}
