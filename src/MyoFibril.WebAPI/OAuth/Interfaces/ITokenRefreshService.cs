using MyoFibril.Contracts.Strava.Responses.OAuth;

namespace MyoFibril.WebAPI.OAuth.Interfaces;

public interface ITokenRefreshService
{
    Task<NewAccessTokenResponse> RefreshAccessToken();
}