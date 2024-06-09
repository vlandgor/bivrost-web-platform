namespace WebApplication1.Models;

public class StudentViewModel(Project project, Session session, Student student)
{
    public Project Project { get; } = project;
    public Session Session { get; } = session;
    public Student Student { get; } = student;
}