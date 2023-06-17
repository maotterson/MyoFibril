using MongoDB.Driver;
using MyoFibril.Contracts.Common.Exceptions;
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

    public async Task<bool> CreateActivity(ActivityEntity activityEntity)
    {
        try
        {
            await _activitiesCollection.InsertOneAsync(activityEntity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ActivityEntity> GetActivityById(string id)
    {
        var activitiesQuery = await _activitiesCollection.Find(a => a.Id == id).ToListAsync();

        if (activitiesQuery is null || activitiesQuery.Count != 1) throw new ActivityNotFoundException();
        var activity = activitiesQuery.SingleOrDefault();

        return activity!;
    }
}
