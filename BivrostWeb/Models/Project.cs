namespace WebApplication1.Models;

public class Project(string id, string fullName, string shortName)
{
    public string Id { get; set; } = id;
    public string FullName { get; set; } = fullName;
    public string ShortName { get; set; } = shortName;
    public List<Session> Sessions { get; set; } = new List<Session>();  
}