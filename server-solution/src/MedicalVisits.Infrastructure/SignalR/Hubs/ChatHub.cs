using Microsoft.AspNetCore.SignalR;

namespace MedicalVisits.Infrastructure.SignalR.Hubs;

public class ChatHub : Hub
{
    public static int TotalViews { get; set; } = 0;


    public async Task NewWindowsLoad()
    {
        TotalViews++;

        await Clients.All.SendAsync("updateTotalViews", TotalViews);
        
    }
}
