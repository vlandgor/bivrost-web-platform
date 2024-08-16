using System.Net.WebSockets;
using BivrostWeb.Server.Packets;

namespace BivrostWeb.Server
{
    public class WebSocketService(ILogger<WebSocketService> logger)
    {
        public const string REQUEST_PATH = "/wa";
        
        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.Request.Path == REQUEST_PATH)
            {
                using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("WebSocket connection established.");
                byte[] buffer = new byte[1024 * 4];

                while (webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

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
                        if (Server.packetHandlers.ContainsKey(packetId))
                        {
                            Console.WriteLine($"Key detected");
                            Server.packetHandlers[packetId](packet);
                        }
                        else
                        {
                            Console.WriteLine($"Unknown packet ID: {packetId}");
                        }
                    }
                }
            }
        }
    }
}