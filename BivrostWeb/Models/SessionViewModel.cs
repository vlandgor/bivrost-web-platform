namespace WebApplication1.Models;

public class SessionViewModel(Project project, Session session)
{
    public Project Project { get; } = project;
    public Session Session { get; } = session;
}