namespace WebApplication1.Models;

public class Project
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public List<Session> Sessions { get; set; }
    
    public Project(string id, string fullName, string shortName, List<Session> sessions)
    {
        Id = id;
        FullName = fullName;
        ShortName = shortName;
        Sessions = sessions;
    }
}