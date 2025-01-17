using MedicalVisits.Infrastructure.Persistence.MongoDb;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.EntitiesMongoDb;
using MongoDB.Driver;

namespace MedicalVisits.Infrastructure.Services.MessagesService;

public class MessagesService : IMessagesService
{
    private readonly MongoDbContext _messages;

    public MessagesService(MongoDbContext messages)
    {
        _messages = messages;
    }

    public async Task AddMessageAsync(Message message)
    {
        try
        {
            await _messages.Messages.InsertOneAsync(message);
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
        var cursor = await _messages.Messages.FindAsync(x => x.ChatId == chatId);
        return await cursor.ToListAsync();
    }
}
