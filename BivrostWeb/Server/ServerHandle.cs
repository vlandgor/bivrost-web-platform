namespace BivrostWeb.Server;

public class ServerHandle
{
    public static void WelcomeReceived(string clientId, Packet packet)
    {
        string clientIdCheck = packet.ReadString();
    }
}