using WebApplication1.Models;

namespace BivrostWeb.Models.ViewModels;

public class SessionViewModel(Project project, Session session)
{
    public Project Project { get; } = project;
    public Session Session { get; } = session;
}