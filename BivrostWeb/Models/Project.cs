namespace WebApplication1.Models;

public class Project(string id, string name, string shortName)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string ShortName { get; set; } = shortName;
    public List<Session> Sessions { get; set; } = new List<Session>();  
}