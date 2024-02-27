namespace WebApplication1.Models;

public class SessionViewModel
{
    public int ProjectId { get; }
    public List<SessionStudentViewModel> Students { get; }
    
    public SessionViewModel(int projectId, List<SessionStudentViewModel> students)
    {
        ProjectId = projectId;
        Students = students;
    }
}