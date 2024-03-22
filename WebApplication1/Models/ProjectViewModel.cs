namespace WebApplication1.Models;

public class ProjectViewModel
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public List<Session> Sessions { get; private set; }
    
    public ProjectViewModel(string id, string name, List<Session> sessions)
    {
        Id = id;
        Name = name;
        Sessions = sessions;
    }
}