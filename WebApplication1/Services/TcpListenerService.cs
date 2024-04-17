using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebApplication1.Hub;

public class TcpListenerService : BackgroundService
{
    private readonly ILogger<TcpListenerService> _logger;
    private readonly IHubContext<KeepAliveHub> _hubContext;
    private TcpListener _tcpListener;

    private static ConcurrentDictionary<string, TcpClient> _clients = new();

    public TcpListenerService(ILogger<TcpListenerService> logger, IHubContext<KeepAliveHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
        _tcpListener = new TcpListener(IPAddress.Any, 5000); // Listening on port 5000
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _tcpListener.Start();
        _logger.LogInformation("TCP listener started on port 5000.");

        while (!stoppingToken.IsCancellationRequested)
        {
            TcpClient client = await _tcpListener.AcceptTcpClientAsync();
            _logger.LogInformation("Client connected.");
            _ = HandleClient(client, stoppingToken);
        }
    }

    private async Task HandleClient(TcpClient client, CancellationToken stoppingToken)
    {
        string clientId = Guid.NewGuid().ToString(); // Generate a unique ID for the client
        if (_clients.TryAdd(clientId, client))
        {
            _logger.LogInformation($"Client {clientId} added.");
        }

        try
        {
            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Initial read for client identification
            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken);
            string initialMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
            clientId = initialMessage; // Use the initial message as client ID

            await _hubContext.Clients.All.SendAsync("ClientConnected", clientId, "Connected");

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken)) > 0)
            {
                var message = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                _logger.LogInformation($"Received from {clientId}: {message}");

                // Update the keep-alive status
                await _hubContext.Clients.All.SendAsync("UpdateKeepAlive", clientId, message);

                // Inform the web client about the ongoing connection
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling client.");
        }
        finally
        {
            if (_clients.TryRemove(clientId, out _))
            {
                _logger.LogInformation($"Client {clientId} removed.");
                await _hubContext.Clients.All.SendAsync("ClientDisconnected", clientId);
            }
        }
    }
}