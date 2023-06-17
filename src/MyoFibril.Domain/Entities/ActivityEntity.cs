using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyoFibril.Domain.Entities;

public class ActivityEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset DateCreated { get; set; }
    public long? StravaActivityId { get; set; }
    public List<PerformedExerciseEntity>? PerformedExercises { get; set; }
}
