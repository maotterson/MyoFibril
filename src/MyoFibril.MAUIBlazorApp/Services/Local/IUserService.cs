using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IUserService
{
    Task Logout();
    Task<bool> CheckLoginCredentials(string username, string password);
    Task<RegisterNewUserResponse> CreateAccountWithCredentials(UserRegisterCredentials credentials);
}