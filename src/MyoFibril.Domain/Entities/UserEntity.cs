using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyoFibril.Domain.Entities;
public class UserEntity
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? StravaAccount { get; set; }
    public string? StravaRefreshToken { get; set; }
    public string? StravaAccessToken { get; set; }
}