using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace MedicalVisits.Infrastructure.SignalR.Hubs;

public class ChatHub : Hub
{
    private static int TotalViews { get; set; } = 0;
    private static int TotalUsers { get; set; } = 0;
    
    private readonly ILogger<ChatHub> _logger;
    
    
    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        TotalUsers++;
         Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
        return base.OnConnectedAsync();
    }

    public override  Task OnDisconnectedAsync(Exception? exception)
    {
        TotalUsers--;
         Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
         
        return base.OnDisconnectedAsync(exception);
    }

    public async Task NewWindowLoaded()
    {
        TotalViews++;

        _logger.LogInformation(
            $"New window loaded. Total views: {TotalViews}",
            TotalViews);
        
        
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
}