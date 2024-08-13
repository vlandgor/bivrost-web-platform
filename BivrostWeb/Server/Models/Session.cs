namespace BivrostWeb.Server.Models;

public class Session
{
    public string sessionId;

    public List<Student> students = new();
    
    public Session(string id)
    {
        sessionId = id;
    }
}