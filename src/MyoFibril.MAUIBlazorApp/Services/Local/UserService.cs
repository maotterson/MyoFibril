using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.Domain.Entities;
using MyoFibril.MAUIBlazorApp.Auth;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public class UserService : IUserService
{
    private readonly CustomAuthenticationStateProvider _authenticationProvider;
    private readonly IStorageService _storageService;
    public UserService(CustomAuthenticationStateProvider authenticationProvider, IStorageService storageService)
    {
        _authenticationProvider = authenticationProvider;
        _storageService = storageService;
        
    }

    public async Task<bool> CheckLoginCredentials(string username, string password)
    {
        var isSuccessfulLogin = await _authenticationProvider.Login(username, password);
        return isSuccessfulLogin;
    }

    public async Task Logout()
    {
        await _authenticationProvider.Logout();
    }
    public async Task<UserInfo> GetLoggedInUserInfo()
    {
        // todo method to retrieve full user entity
        var userInfo = await _storageService.GetItemAsync<UserInfo>("user_info");
        return userInfo;
    }
    public async Task<UserEntity> GetLoggedInUser()
    {
        var userInfo = await GetLoggedInUserInfo();

        // retrieve user entity information via a repository/caching paradigm
        return new UserEntity
        {
            Username = userInfo.Username,
            Email = userInfo.Email
        };
    }
}