namespace WebApplication1.Models;

public class Session
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public bool Status { get; private set; }
    public string Date { get; private set; }
    public int Players { get; private set; }
    public float Duration { get; private set; }
    
    public Session(string id, string name, bool status, string date, int players, float duration)
    {
        Id = id;
        Name = name;
        Status = status;
        Date = date;
        Players = players;
        Duration = duration;
    }
}