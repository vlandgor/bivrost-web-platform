namespace WebApplication1.Models;

public class SessionViewModel
{
    public string SessionId { get; }
    public string SessionName { get; }
    public List<Student> Students { get; }
    
    public SessionViewModel(string sessionId, string sessionName, List<Student> students)
    {
        SessionId = sessionId;
        SessionName = sessionName;
        Students = students;
    }
}