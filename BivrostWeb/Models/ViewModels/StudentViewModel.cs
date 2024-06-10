using WebApplication1.Models;

namespace BivrostWeb.Models.ViewModels;

public class StudentViewModel(Project project, Session session, Student student)
{
    public Project Project { get; } = project;
    public Session Session { get; } = session;
    public Student Student { get; } = student;
}