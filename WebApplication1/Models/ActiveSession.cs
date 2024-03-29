namespace WebApplication1.Models;

public class ActiveSession
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public bool Status { get; private set; }
    public int Students { get; private set; }
}