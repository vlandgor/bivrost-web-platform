namespace WebApplication1.Models;

public class SessionViewModel(string projectId, string sessionId, string sessionName, List<Student> students)
{
    public string ProjectId { get; } = projectId;
    public string SessionId { get; } = sessionId;
    public string SessionName { get; } = sessionName;
    public List<Student> Students { get; } = students;
}