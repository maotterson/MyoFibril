using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.GetActivity;
using Refit;

namespace MyoFibril.WebAPI.Strava.Api;

[Headers("Authorization: Bearer")]
public interface IStravaApi
{
    [Headers("Accept: application/json")]
    [Get("/activities/{id}")]
    Task<ApiResponse<StravaGetActivityResponse>> GetActivityById(string id);

    [Headers("Content-Type: application/json")]
    [Post("/activities")]
    Task<ApiResponse<StravaCreateActivityResponse>> CreateActivity([Body(BodySerializationMethod.Serialized)] StravaCreateActivityRequest activity);
}
