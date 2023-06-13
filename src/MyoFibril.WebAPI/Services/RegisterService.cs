using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class RegisterService : IRegisterService
{
    private readonly ICredentialsRepository _credentialsRepository;
    private readonly IJwtService _jwtService;

    public RegisterService(ICredentialsRepository credentialsRepository, IJwtService jwtService)
    {
        _credentialsRepository = credentialsRepository;
        _jwtService = jwtService;

    }
    public async Task<RegisterNewUserResponse> RegisterNewUser(RegisterNewUserRequest request)
    {
        // verify that username doesnt exist
        var usernameExists = await _credentialsRepository.DoesUsernameExist(request.Username);
        if (usernameExists) throw new UsernameExistsException();

        // check any other potential rules for registration (e.g. password length, username length etc)
        if (string.IsNullOrEmpty(request.Username) ||
            string.IsNullOrEmpty(request.Password) ||
            string.IsNullOrEmpty(request.Email))
            throw new InvalidCredentialsException();

        // add credentials to database
        var generatedSalt = BCrypt.Net.BCrypt.GenerateSalt();
        var protectedCredentials = new ProtectedUserCredentials(username: request.Username, password: request.Password, salt: generatedSalt, email: request.Email);
        await _credentialsRepository.RegisterNewUserWithProtectedCredentials(protectedCredentials);

        // use credentials to get an access and refresh token
        var createdCredentials = await _credentialsRepository.GetCredentialsForUsername(protectedCredentials.Username);
        var (accessToken, refreshToken) = await _jwtService.GetTokensWithCredentials(createdCredentials);

        var response = new RegisterNewUserResponse
        {
            Success = true,
            TokenInfo = new GetAccessTokenResponse { AccessToken = accessToken, RefreshToken = refreshToken },
            UserInfo = new UserInfo { Username = createdCredentials.Username, Email = createdCredentials.Email }
        };

        return response;
    }
}
