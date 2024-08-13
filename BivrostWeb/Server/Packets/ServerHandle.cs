namespace BivrostWeb.Server.Packets;

public class ServerHandle
{
    public static event Action<string, string> OnStudentLocked;
    public static event Action<string, string, int> OnStudentProgressUpdated;
    
    public static void LockStudent(Packet packet)
    {
        string sessionId = packet.ReadString();
        string studentId = packet.ReadString();
        
        Console.WriteLine("Server Handle | Lock Student | Method Called");
        
        OnStudentLocked?.Invoke(sessionId, studentId);
    }
    
    public static void UpdateStudentProgress(Packet packet)
    {
        string sessionId = packet.ReadString();
        string studentId = packet.ReadString();
        int studentProgress = packet.ReadInt();
        
        Console.WriteLine("Server Handle | Lock Student | Method Called");
        
        OnStudentProgressUpdated?.Invoke(sessionId, studentId, studentProgress);
    }
}