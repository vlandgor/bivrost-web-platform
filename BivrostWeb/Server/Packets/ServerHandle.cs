namespace BivrostWeb.Server.Packets;

public class ServerHandle
{
    public static void LockStudent(Packet packet)
    {
        StudentData studentData = new StudentData();
        
        GetStudentData(packet, ref studentData);
    }
    
    public static void UpdateStudentProgress(Packet packet)
    {
        StudentData studentData = new StudentData();
        
        //GetStudentData(packet);
        int studentProgress = packet.ReadInt();
    }

    private static StudentData GetStudentData(Packet packet, ref StudentData studentData)
    {
        studentData.projectId = packet.ReadString(); 
        studentData.sessionId = packet.ReadString();
        studentData.studentId = packet.ReadString();

        return studentData;
    }
}