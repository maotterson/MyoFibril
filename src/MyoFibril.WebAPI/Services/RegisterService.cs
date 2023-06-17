using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.Domain.Entities;
using MyoFibril.WebAPI.Models.Auth;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Services;

public class RegisterService : IRegisterService
{
    private readonly ICredentialsRepository _credentialsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public RegisterService(ICredentialsRepository credentialsRepository, IUserRepository userRepository, IJwtService jwtService)
    {
        _credentialsRepository = credentialsRepository;
        _userRepository = userRepository;
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

        // add created user to database
        var user = new UserEntity
        {
            Username = request.Username,
            Email = request.Email,
            StravaAccount = null
        };
        await _userRepository.CreateUser(user);

        // use credentials to get an access and refresh token
        var createdCredentials = await _credentialsRepository.GetCredentialsForUsername(protectedCredentials.Username);
        var (accessToken, refreshToken) = await _jwtService.GetTokensWithCredentials(createdCredentials);
        await _credentialsRepository.UpdateRefreshTokenForUsername(protectedCredentials.Username, refreshToken);

        var response = new RegisterNewUserResponse
        {
            Success = true,
            TokenInfo = new GetAccessTokenResponse { AccessToken = accessToken, RefreshToken = refreshToken },
            UserInfo = new UserInfo { Username = createdCredentials.Username, Email = createdCredentials.Email }
        };

        return response;
    }
}
