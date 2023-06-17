using MongoDB.Driver;
using MyoFibril.Domain.Entities;
using MyoFibril.WebAPI.Repositories.Interfaces;

namespace MyoFibril.WebAPI.Repositories;

public class ActivityRepository : IActivityRepository
{
    private const string COLLECTION_NAME = "activities";
    private readonly IMongoCollection<ActivityEntity> _activitiesCollection;
    private readonly IConfiguration _configuration;

    public ActivityRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        _configuration = configuration;
        _activitiesCollection = mongoClient.GetDatabase(_configuration["Database:DatabaseName"]).GetCollection<ActivityEntity>(COLLECTION_NAME);
    }

    public Task<bool> CreateActivity(ActivityEntity activityEntity)
    {
        throw new NotImplementedException();
    }

    public Task<ActivityEntity> GetActivityById(string id)
    {
        throw new NotImplementedException();
    }
}
