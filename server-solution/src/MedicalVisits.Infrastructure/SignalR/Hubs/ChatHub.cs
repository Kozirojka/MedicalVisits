using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace MedicalVisits.Infrastructure.SignalR.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
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

    public async Task SendMessage(string user,string text)
    {
        _logger.LogInformation("Wen in send message method on server");
        await Clients.All.SendAsync("ReceiveMessage", user, text);
    }
    
    
}