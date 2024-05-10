namespace BivrostWeb.Server;

public class ServerSend
{
    private static void SendData(string clientId, Packet packet)
    {
        packet.WriteLength();
        
    }

    public static void Welcome(string clientId, string message)
    {
        using Packet packet = new Packet((int)ServerPackets.welcome);
        
        packet.Write(clientId);
        packet.Write(message);
        SendData(clientId, packet);
    }
}