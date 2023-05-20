using MyoFibril.Contracts.Strava.Responses;

namespace MyoFibril.WebAPI.OAuth;

public interface ITokenRefreshService
{
    Task<NewAccessTokenResponse> RefreshAccessToken();
}