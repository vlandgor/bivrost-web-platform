namespace BivrostWeb.Hub;

public class SessionHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task CreateStudent(bool locked, string studentId, string studentName, bool studentStatus, int studentProgress)
    {
        
    }

    public async Task LockStudent(string studentId)
    {
        
    }
    
    public async Task UpdateStudentProgress(string studentId, int progress)
    {
        
    }
}