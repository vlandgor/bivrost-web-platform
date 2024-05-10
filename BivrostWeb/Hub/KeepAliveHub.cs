using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hub;

public class KeepAliveHub : Microsoft.AspNetCore.SignalR.Hub
{
    // Method to handle when a client connects to the Hub
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("SendAction", $"{Context.ConnectionId} joined");
        await base.OnConnectedAsync();
    }

    // Method to handle when a client disconnects from the Hub
    public override async Task OnDisconnectedAsync(System.Exception exception)
    {
        await Clients.All.SendAsync("SendAction", $"{Context.ConnectionId} left");
        await base.OnDisconnectedAsync(exception);
    }

    // A method to broadcast a keep-alive message received from any client
    public async Task UpdateKeepAlive(string clientId, string message)
    {
        // Broadcast a keep-alive message to all connected clients
        await Clients.All.SendAsync("UpdateKeepAlive", clientId, message);
    }

    // Method to notify all clients when a new client is connected
    public async Task NotifyClientConnected(string clientId, string initialMessage)
    {
        // Notify all clients that a new client has connected
        await Clients.All.SendAsync("ClientConnected", clientId, initialMessage);
    }

    // Method to notify all clients when a client is disconnected
    public async Task NotifyClientDisconnected(string clientId)
    {
        // Notify all clients that a client has disconnected
        await Clients.All.SendAsync("ClientDisconnected", clientId);
    }
}