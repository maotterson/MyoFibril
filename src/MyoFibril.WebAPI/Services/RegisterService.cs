using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class RegisterService : IRegisterService
{
    private readonly ICredentialsRepository _credentialsRepository;

    public RegisterService(ICredentialsRepository credentialsRepository)
    {
        _credentialsRepository = credentialsRepository;
    }
    public async Task<RegisterNewUserResponse> RegisterNewUser(RegisterNewUserRequest request)
    {
        // verify that username doesnt exist
        var usernameExists = await _credentialsRepository.DoesUsernameExist(request.Username);
        if (usernameExists) throw new InvalidCredentialsException();

        // check any other potential rules for registration (e.g. password length, username length etc)
        if (string.IsNullOrEmpty(request.Username) ||
            string.IsNullOrEmpty(request.Password) ||
            string.IsNullOrEmpty(request.Email))
            throw new InvalidCredentialsException();

        // add credentials to database
        var generatedSalt = BCrypt.Net.BCrypt.GenerateSalt();
        var protectedCredentials = new ProtectedUserCredentials(username: request.Username, password: request.Password, salt: generatedSalt, email: request.Email);
        var registeredUserInfo = await _credentialsRepository.RegisterNewUserWithProtectedCredentials(protectedCredentials);

        // use credentials to get an access and refresh token
    }
}
