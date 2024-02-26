namespace WebApplication1.Models;

public class Session
{
    public string ProjectName { get; }
    
    public Session(string projectName)
    {
        ProjectName = projectName;
    }
}