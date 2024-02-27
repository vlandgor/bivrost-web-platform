namespace WebApplication1.Models;

public class Session
{
    public string ProjectName { get; }
    public List<Student> Students { get; }
    
    public Session(string projectName, List<Student> students)
    {
        ProjectName = projectName;
        Students = students;
    }
}