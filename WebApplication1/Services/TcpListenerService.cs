using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hub;

namespace WebApplication1.Services;

public class TcpListenerService : BackgroundService
{
    private readonly ILogger<TcpListenerService> _logger;
    private readonly IHubContext<KeepAliveHub> _hubContext;
    private TcpListener _tcpListener;

    // Use a concurrent dictionary to track connected clients
    private static ConcurrentDictionary<string, TcpClient> _clients = new ConcurrentDictionary<string, TcpClient>();

    public TcpListenerService(ILogger<TcpListenerService> logger, IHubContext<KeepAliveHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
        _tcpListener = new TcpListener(IPAddress.Any, 5000); // Listen on port 5000
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _tcpListener.Start();
        _logger.LogInformation("TCP listener started on port 5000.");

        while (!stoppingToken.IsCancellationRequested)
        {
            TcpClient client = await _tcpListener.AcceptTcpClientAsync();
            _logger.LogInformation("Client connected.");
            
            // Handle the client and track it
            _ = Task.Run(() => HandleClient(client, stoppingToken));
        }
    }

    private async Task HandleClient(TcpClient client, CancellationToken stoppingToken)
    {
        string clientId = Guid.NewGuid().ToString(); // Generate a unique ID for the client
        _clients.TryAdd(clientId, client);

        try
        {
            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken)) > 0)
            {
                var message = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                _logger.LogInformation($"Received: {message}");

                // Update the keep-alive status
                await _hubContext.Clients.All.SendAsync("UpdateKeepAlive", clientId, message);

                // You can also inform the web client about the new connection here
                await _hubContext.Clients.All.SendAsync("ClientConnected", clientId, message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling client.");
        }
        finally
        {
            _clients.TryRemove(clientId, out _); // Remove client from the dictionary when disconnected
            await _hubContext.Clients.All.SendAsync("ClientDisconnected", clientId);
        }
    }
}