using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;

namespace MyoFibril.WebAPI.Repositories.Interfaces;

public interface IActivityRepository
{
    Task<CreateActivityResponse> CreateActivity(CreateActivityRequest request);
    Task<GetActivityResponse> GetActivityById(string id);
}
