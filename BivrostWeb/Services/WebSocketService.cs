using System.Collections.Concurrent;
using System.Net.WebSockets;
using BivrostWeb.Server.Packets;

namespace BivrostWeb.Services
{
    public class WebSocketService
    {
        private static readonly ConcurrentBag<string> _messages = new();
        
        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.Request.Path == "/ws" && context.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("WebSocket connection established.");
                var buffer = new byte[1024 * 4];

                while (webSocket.State == WebSocketState.Open)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        Console.WriteLine("WebSocket connection closed.");
                        break;
                    }

                    // Extracting the exact byte array from the buffer
                    byte[] bytes = new byte[result.Count];
                    Array.Copy(buffer, 0, bytes, 0, result.Count);
                    Console.WriteLine($"Received message as bytes: {BitConverter.ToString(bytes)}");
                    
                    using (Packet packet = new Packet(bytes))
                    {
                        int packetLength = packet.ReadInt();
                        int packetId = packet.ReadInt();
                        // Ensure the packet ID is valid and that the packet isn't a duplicate
                        if (Server.Server.packetHandlers.ContainsKey(packetId))
                        {
                            Console.WriteLine($"Key detected");
                            Server.Server.packetHandlers[packetId](packet);
                        }
                        else
                        {
                            Console.WriteLine($"Unknown packet ID: {packetId}");
                        }
                    }

                    // Optional: you can decide to send a response back to the client to acknowledge receipt
                    // await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                }
            }
        }

        public List<string> GetMessages()
        {
            return _messages.ToList();
        }
    }
}