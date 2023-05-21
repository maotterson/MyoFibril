using MyoFibril.Contracts.Strava.CreateActivity;
using MyoFibril.Contracts.Strava.GetActivity;
using Refit;

namespace MyoFibril.WebAPI.Strava.Api;

[Headers("Authorization: Bearer")]
public interface IStravaApi
{
    [Headers("Accept: application/json")]
    [Get("/activities/{id}")]
    Task<ApiResponse<GetActivityResponse>> GetActivityById(string id);

    [Headers("Content-Type: application/json")]
    [Post("/activities")]
    Task<ApiResponse<CreateActivityResponse>> CreateActivity([Body(BodySerializationMethod.Serialized)] CreateActivityRequest activity);
}
