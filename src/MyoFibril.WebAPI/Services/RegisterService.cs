using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class RegisterService : IRegisterService
{
    public async Task<RegisterNewUserResponse> RegisterNewUser(RegisterNewUserRequest request)
    {
        // verify that username doesnt exist

        // add credentials to database

        // use credentials to get an access and refresh token
    }
}
