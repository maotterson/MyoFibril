using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyoFibril.Domain.Entities.Auth;
public class UserCredentialsEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Username { get; set; } = default!;
    public string HashedPassword { get; set; } = default!;
    public string Salt { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? RefreshToken { get; set; } = default!;
}