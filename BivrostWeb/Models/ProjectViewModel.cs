namespace WebApplication1.Models;

public class ProjectViewModel(string id, string fullName, List<Session> sessions)
{
    public string Id { get; private set; } = id;
    public string FullName { get; private set; } = fullName;
    public List<Session> Sessions { get; private set; } = sessions;
}