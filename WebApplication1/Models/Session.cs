namespace WebApplication1.Models;

public class Session
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public bool Status { get; private set; }
    public int Students { get; private set; }
    
    public Session(string id, string name, bool status, int students)
    {
        Id = id;
        Name = name;
        Status = status;
        Students = students;
    }
}