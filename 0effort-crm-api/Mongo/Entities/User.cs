using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _0effort_crm_api.Mongo.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}