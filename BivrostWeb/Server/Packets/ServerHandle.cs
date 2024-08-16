namespace BivrostWeb.Server.Packets;

public class ServerHandle(Server server)
{
    public async void LockStudent(Packet packet)
    {
        string sessionId = packet.ReadString();
        string studentId = packet.ReadString();

        await server.LockStudent(sessionId, studentId);
    }
}