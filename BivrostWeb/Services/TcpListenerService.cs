﻿using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BivrostWeb.Server;
using WebApplication1.Hub;

public class TcpListenerService : BackgroundService
{
    private const int TCP_PORT = 3000;
    private const int CLIENT_BUFFER = 1024;
    
    private readonly ILogger<TcpListenerService> _logger;
    private readonly IHubContext<KeepAliveHub> _hubContext;
    private TcpListener _tcpListener;

    public delegate void PacketHandler(string clientId, Packet packet);
    public static Dictionary<int, PacketHandler> packetHandlers;

    private static ConcurrentDictionary<string, Client> _clients = new();

    public TcpListenerService(ILogger<TcpListenerService> logger, IHubContext<KeepAliveHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;

        InitializeServerData();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _tcpListener = new TcpListener(IPAddress.Any, TCP_PORT);
        _tcpListener.Start();

        while (!stoppingToken.IsCancellationRequested)
        {
            TcpClient client = await _tcpListener.AcceptTcpClientAsync();
            await HandleClient(client, stoppingToken);
        }
    }

    private async Task HandleClient(TcpClient tcpClient, CancellationToken stoppingToken)
    {
        string clientId = Guid.NewGuid().ToString(); // Generate a unique ID for the client
        
        Client client = new Client(clientId);
        client.tcp.Connect(tcpClient);
        
        if (_clients.TryAdd(clientId, client))
        {
            _logger.LogInformation($"Client {clientId} added.");
        }

        try
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[CLIENT_BUFFER];

            // Initial read for client identification
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken);
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
    private void InitializeServerData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived }
        };
    }
}