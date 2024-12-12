using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Dtos.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalVisits.Application.Chat.Queries.GetAllRelatedChat;

public class GetAllChatQueryHandler : IRequestHandler<GetAllChatQuery, List<ChatResponce>>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GetAllChatQueryHandler> _logger;
    
    public GetAllChatQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllChatQueryHandler> logger)
    {
        this.dbContext = dbContext;
        this._logger = logger;
    }

    public async Task<List<ChatResponce>> Handle(GetAllChatQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var participants = await dbContext.ChatParticipants
                .Where(u => u.UserId == request.userId)
                .Include(cp => cp.Chat)
                .ToListAsync((cancellationToken));

            if (participants == null || participants.Count == 0)
            {
                _logger.LogWarning("No chats found for user with ID: {UserId}", request.userId);
                return new List<ChatResponce>();
            }

            var chatResponses = participants.Select(p => new ChatResponce
            {
                Id = p.Chat.Id,
                Name = p.Chat.Name,
                Type = p.Chat.Type,
            }).ToList();

            return chatResponses;
        }
        catch (Exception ex)
        {
            // Логування помилки
            _logger.LogError(ex, "An error occurred while retrieving chats for user with ID: {UserId}", request.userId);

            // Перекидаємо виняток далі або обробляємо його відповідним чином
            throw new Exception("An error occurred while retrieving chats. Please try again later.", ex);
        }
    }
}
