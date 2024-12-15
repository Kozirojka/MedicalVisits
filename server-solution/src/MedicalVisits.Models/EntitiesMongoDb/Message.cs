using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicalVisits.Models.EntitiesMongoDb;

public class Message
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public int ChatId { get; set; }
    
    public string? SenderId { get; set; }
    
    public string MessageText { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
}
