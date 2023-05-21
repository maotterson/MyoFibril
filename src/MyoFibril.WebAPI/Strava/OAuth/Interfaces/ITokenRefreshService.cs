using MyoFibril.Contracts.Strava.OAuth;

namespace MyoFibril.WebAPI.Strava.OAuth.Interfaces;

public interface ITokenRefreshService
{
    Task<NewAccessTokenResponse> RefreshAccessToken();
}