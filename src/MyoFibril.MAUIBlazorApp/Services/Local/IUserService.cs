using MyoFibril.Contracts.WebAPI.Auth;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IUserService
{
    Task Logout();
    Task<bool> CheckLoginCredentials(string username, string password);
    Task<UserEntity> GetLoggedInUser();
}