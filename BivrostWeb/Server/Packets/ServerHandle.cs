﻿namespace BivrostWeb.Server.Packets;

public class ServerHandle(Server server)
{
    public async void LockStudent(Packet packet)
    {
        string sessionId = packet.ReadString();
        string studentId = packet.ReadString();

        await server.LockStudent(sessionId, studentId);
    }

    public async void KeepAliveStudentUpdate(Packet packet)
    {
        string sessionId = packet.ReadString();
        string studentId = packet.ReadString();
        bool isAlive = packet.ReadBool();

        await server.KeepAliveStudentUpdate(sessionId, studentId, isAlive);
    }
    
    public async void UpdateStudentProgress(Packet packet)
    {
        string sessionId = packet.ReadString();
        string studentId = packet.ReadString();
        int progress = packet.ReadInt();

        await server.UpdateStudentProgress(sessionId, studentId, progress);
    }
}