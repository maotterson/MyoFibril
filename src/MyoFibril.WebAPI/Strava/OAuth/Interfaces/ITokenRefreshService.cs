using MyoFibril.Contracts.Strava.OAuth;

namespace MyoFibril.WebAPI.OAuth.Interfaces;

public interface ITokenRefreshService
{
    Task<NewAccessTokenResponse> RefreshAccessToken();
}