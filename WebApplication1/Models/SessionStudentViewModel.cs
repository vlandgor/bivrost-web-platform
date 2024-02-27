namespace WebApplication1.Models;

public enum ConnectionStatus
{
    Connected,
    Disconnected
}

public class SessionStudentViewModel
{
    public string Name { get; set; }
    public ConnectionStatus ConnectionStatus { get; set; }
    public string Duration { get; set; }
    public string Progress { get; set; }
}