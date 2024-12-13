using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace MedicalVisits.Infrastructure.SignalR.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;

    public async Task JoinGroup(int chatId)
    {
        _logger.LogInformation($"Enter in room message on {chatId}");
        
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", "System",
            $"'{Context.ConnectionId} has been connected to chat");
    }


    public async Task SendMessageToGroup(int chatId, string message)
    {
        string senderId = Context.ConnectionId;
        
        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", senderId, message);  
    }
    
    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
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