using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MedicalVisits.Infrastructure.SignalR.Hubs;

public class ChatHub : Hub
{
    private static int TotalViews { get; set; } = 0;
    private readonly ILogger<ChatHub> _logger;
    
    
    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
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