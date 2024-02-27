namespace WebApplication1.Models;

public enum ConnectionStatus
{
    Connected,
    Disconnected
}

public class Student
{
    public string Name { get; set; }
    public ConnectionStatus ConnectionStatus { get; set; }
    public string Duration { get; set; }
    public string Progress { get; set; }
}