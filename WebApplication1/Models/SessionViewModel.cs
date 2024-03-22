namespace WebApplication1.Models;

public class SessionViewModel
{
    public int ProjectId { get; }
    public List<SessionStudentViewModel> Students { get; }
    public int StudentsAmount => Students.Count;
    
    public SessionViewModel(int projectId, List<SessionStudentViewModel> students)
    {
        ProjectId = projectId;
        Students = students;
    }
}