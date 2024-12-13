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
            var userChats = await dbContext.ChatParticipants
                .Where(cp => cp.UserId == request.userId)
                .Include(cp => cp.Chat).ThenInclude(chat => chat.Participants)
                .ThenInclude(chatParticipants => chatParticipants.User)
                .ToListAsync(cancellationToken);
    
            if (!userChats.Any())
            {
                _logger.LogWarning("No chats found for user with ID: {UserId}", request.userId);
                return new List<ChatResponce>();
            }
    
            var chatResponses = userChats.SelectMany(uc =>
                uc.Chat.Participants
                    .Where(p => p.UserId != request.userId) 
                    .Select(participant => new ChatResponce
                    {
                        Id = uc.Chat.Id.Value,
                        Name = $"{participant.User.FirstName} {participant.User.LastName}",
                        Type = uc.Chat.Type,
                    })
            ).ToList();
    
            return chatResponses;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving chats for user with ID: {UserId}", request.userId);
            throw new Exception("An error occurred while retrieving chats. Please try again later.", ex);
        }
    }

}
