using MyoFibril.Contracts.WebAPI.Auth;

namespace MyoFibril.WebAPI.Services.Interfaces;

public interface IRegisterService
{
    Task<RegisterNewUserResponse> RegisterNewUser(RegisterNewUserRequest request);
}
