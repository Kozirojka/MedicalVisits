using MedicalVisits.Models.EntitiesMongoDb;

namespace MedicalVisits.Infrastructure.Services.MessagesService;

public interface IMessagesService
{
    public Task AddMessageAsync(Message message);
    public Task<List<Message>> GetAllRelatedToChatIdMessagesAsync(int chatId);
}
