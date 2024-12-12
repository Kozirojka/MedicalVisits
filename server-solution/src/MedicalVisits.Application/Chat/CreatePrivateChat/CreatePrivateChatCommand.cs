using MediatR;
using MedicalVisits.Models.Auth;
using MedicalVisits.Models.Dtos.AuthDto;

namespace MedicalVisits.Application.Chat.CreatePrivateChat;

public class CreatePrivateChatCommand : IRequest<GeneralRespone>
{
    public CreatePrivateChatCommand(UserDto userDto, string userId)
    {
        UserDto = userDto;
        UserId = userId;
    }
    
    public string UserId { get; init; }
    public UserDto UserDto { get; set; }
}
