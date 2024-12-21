using MedicalVisits.Models.EntitiesMongoDb;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IMessagesService
{
    public Task AddMessageAsync(Message message);
    public Task<List<Message>> GetAllRelatedToChatIdMessagesAsync(int chatId);
}
