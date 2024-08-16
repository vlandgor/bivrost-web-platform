namespace BivrostWeb.Hub;

public class SessionHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task CreateStudent(string studentId, bool locked, string studentName, bool studentStatus, int studentProgress)
    {
        
    }

    public async Task LockStudent(string studentId)
    {
        
    }
    
    public async Task UpdateStudentProgress(string studentId, int progress)
    {
        
    }
}