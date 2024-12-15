using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Admin.Queries.GetAllUser;
using MedicalVisits.Application.Chat.CreatePrivateChat;
using MedicalVisits.Application.Chat.Queries.GetAllRelatedChat;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Infrastructure.SignalR.Hubs;
using MedicalVisits.Models.Dtos.AuthDto;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MedicalVisits.API.Controllers.ChatController;

[ApiController]
[Authorize(Roles = "Doctor, Admin, Patient")]
[Route("api/[controller]")]
public class ChatController : BaseController
{
    private readonly IUserService _userService;
    private readonly IHubContext<ChatHub> _chatHubContext;
    private readonly IMessagesService _messagesService;
    private readonly ApplicationDbContext _dbContext;
    public ChatController(IMediator mediator, UserManager<ApplicationUser> userManager, IUserService userService, IHubContext<ChatHub> chatHubContext, IMessagesService messagesService, ApplicationDbContext dbContext) : base(mediator, userManager)
    {
        _userService = userService;
        _chatHubContext = chatHubContext;
        _messagesService = messagesService;
        _dbContext = dbContext;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var command = new GetAllUsersQuery();

        var usersList = await _Mediator.Send(command);


        if (usersList == null)
        {
            return NotFound();
        }
        

        return Ok(usersList);
    }

    [HttpPost("private-chat")]
    public async Task<IActionResult> CreatePrivateChat(UserDto chatPrivateRequest)
    {
        string? userId = _userService.GetUserId(User);
        if (userId == null)
        {
            return NotFound("main user not found");
        }

        var command = new CreatePrivateChatCommand(chatPrivateRequest, userId);
        var result = await _Mediator.Send(command);


        if (result == null)
            return BadRequest($"Result after CreatePrivateChatCommandHandler is dalse {result}");
        
        
        return Ok(result);
    }


    [HttpGet("chats")]
    public async Task<IActionResult> GetAllRelatedChats()
    {
        string? userId = _userService.GetUserId(User);

        var query = new GetAllChatQuery(userId);
        
        var chatList = await _Mediator.Send(query);

        if (chatList == null)
        {
            return BadRequest("chatList is empty");
        }

        return Ok(chatList);
    }

    [HttpGet("{chatId}/history")]
    public async Task<IActionResult> GetChatMessages(int chatId)
    {

        var exist = _dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
        
        
        if (exist == null)
        {

            return BadRequest("Chut is is null");
        }
        var messagePack = await _messagesService.GetAllRelatedToChatIdMessagesAsync(chatId);

        if (messagePack == null)
        {
            return NotFound("Messages from mongoDb havent fetched");
        }
        
        return Ok(messagePack);
        
    }
}
