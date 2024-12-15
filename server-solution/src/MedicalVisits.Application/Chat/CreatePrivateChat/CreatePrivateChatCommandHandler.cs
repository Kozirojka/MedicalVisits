using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Auth;
using MedicalVisits.Models.Entities.ChatEntities;

namespace MedicalVisits.Application.Chat.CreatePrivateChat;

public class CreatePrivateChatCommandHandler : IRequestHandler<CreatePrivateChatCommand, GeneralRespone>
{   
    private readonly ApplicationDbContext _context;

    public CreatePrivateChatCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GeneralRespone> Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = MedicalVisits.Models.Entities.ChatEntities.Chat.CreatePrivateChat(
            request.UserDto.FirstName + " " + request.UserDto.LastName, 
            request.UserId, 
            request.UserDto.Id 
        );

        await _context.Chats.AddAsync(chat, cancellationToken);

        var saveResult = await _context.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return new GeneralRespone
            {
                Description = "Все погано",
                Code = 400
            };
        }

        return new GeneralRespone
        {
            Description = "Чат успішно створено",
            Code = 200
        };
    }

}
