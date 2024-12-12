using MediatR;
using MedicalVisits.Models.Dtos.Chat;

namespace MedicalVisits.Application.Chat.Queries.GetAllRelatedChat;

public class GetAllChatQuery : IRequest<List<ChatResponce>>
{
    public GetAllChatQuery(string userId)
    {
        this.userId = userId;
    }


    public string userId { get; set; }
}
