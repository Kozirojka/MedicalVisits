using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Configurations;
using MedicalVisits.Models.EntitiesMongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicalVisits.Infrastructure.Services.MessagesService;

public class MessagesService : IMessagesService
{
    private readonly IMongoCollection<Message> _messages;

    public MessagesService(IOptions<MessageDatabaseSettings> messageDatabaseSettings)
    {
        
        var mongoClient = new MongoClient(messageDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(messageDatabaseSettings.Value.DatabaseName);

        _messages = mongoDatabase.GetCollection<Message>(
            messageDatabaseSettings.Value.MessageCollectionName
        );
    }

    public async Task AddMessageAsync(Message message)
    {
        try
        {
            await _messages.InsertOneAsync(message);
            Console.WriteLine("Message successfully added!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add message: {ex.Message}");
            throw;
        }
    }


    public async Task<List<Message>> GetAllRelatedToChatIdMessagesAsync(int chatId)
    {
        var cursor =  await _messages.FindAsync(x => x.ChatId == chatId);
        
        return await cursor.ToListAsync();
    }
    
}
