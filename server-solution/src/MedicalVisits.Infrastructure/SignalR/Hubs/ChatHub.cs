using System.Security.Claims;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.EntitiesMongoDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace MedicalVisits.Infrastructure.SignalR.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly IMessagesService _messagesService;
    private readonly IUserService _userService;
    
    
    public async Task JoinGroup(int chatId)
    {
        _logger.LogInformation($"Enter in room message on {chatId}");
        
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        // await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", "System",
        //     $"'{Context.ConnectionId} has been connected to chat");
    }


    public async Task SendMessageToGroup(int chatId, string message)
    {
        string? senderId =Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;;

        var messageObject = new Message()
        {
            ChatId = chatId,
            SenderId = senderId,
            MessageText = message,
            Timestamp = DateTime.UtcNow
        };
        
        await _messagesService.AddMessageAsync(messageObject);
        
        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", senderId, message);  
    }
    
    public ChatHub(ILogger<ChatHub> logger, IMessagesService messagesService, IUserService userService)
    {
        _logger = logger;
        _messagesService = messagesService;
        _userService = userService;
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override  Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
    
    
    
}