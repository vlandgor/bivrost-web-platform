namespace WebApplication1.Models;

public class SessionViewModel
{
    public string SessionId { get; }
    public string SessionName { get; }
    public List<SessionStudentViewModel> Students { get; }
    
    public SessionViewModel(string sessionId, string sessionName, List<SessionStudentViewModel> students)
    {
        SessionId = sessionId;
        SessionName = sessionName;
        Students = students;
    }
}