using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hub;

public class KeepAliveHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}