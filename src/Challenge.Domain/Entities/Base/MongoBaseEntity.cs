using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Challenge.Domain.Entities.Base;

public class MongoBaseEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
