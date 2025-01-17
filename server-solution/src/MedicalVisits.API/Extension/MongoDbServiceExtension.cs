using MedicalVisits.Infrastructure.Persistence.MongoDb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace MedicalVisits.API.Extension;

public static class MongoDbServiceExtension
{
    public static IServiceCollection AddMongoDbServiceExtension(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        var mongoDbConfiguration = configuration.GetSection("MongoDb");
        var connectionString = mongoDbConfiguration["ConnectionString"];
        var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);

        var mongoClient = new MongoClient(mongoClientSettings);

        try
        {
            mongoClient.ListDatabaseNames();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to connect to MongoDB. Check the connection string and database availability.", ex);
        }

        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddSingleton<MongoDbContext>();

        return services;
    }
}
