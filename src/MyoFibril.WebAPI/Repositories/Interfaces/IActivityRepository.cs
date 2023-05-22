using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;
using MyoFibril.Domain.Entities;

namespace MyoFibril.WebAPI.Repositories.Interfaces;

public interface IActivityRepository
{
    Task<bool> CreateActivity(ActivityEntity activityEntity);
    Task<ActivityEntity> GetActivityById(string id);
}
