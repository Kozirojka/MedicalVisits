using MedicalVisits.Models.EntitiesMongoDb;
using MongoDB.Driver;

namespace MedicalVisits.Infrastructure.Persistence.MongoDb;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient mongoClient)
    {
        _database = mongoClient.GetDatabase("MedicalVisits");
    }

    public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Messages");
}
