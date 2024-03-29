namespace WebApplication1.Models;

public class ProjectViewModel
{
    public string Id { get; private set; }
    public List<Session> Sessions { get; private set; }
    
    public ProjectViewModel(string id, List<Session> sessions)
    {
        Id = id;
        Sessions = sessions;
    }
}