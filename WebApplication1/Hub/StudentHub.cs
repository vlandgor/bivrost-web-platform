using Microsoft.AspNetCore.SignalR;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Hub;

public class StudentHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task CreateStudent(string projectId, string sessionId, string studentId, string studentName)
    {
        await AwsConnectionService.AddNewStudent(projectId, sessionId, new Student(studentId, studentName, 0, false));
        
        // Notify all clients that a new student is added
        await Clients.All.SendAsync("StudentCreated", studentId, studentName, false, 0);
    }
}