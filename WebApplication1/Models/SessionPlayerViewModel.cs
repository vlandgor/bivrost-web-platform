namespace WebApplication1.Models;

public enum ConnectionStatus
{
    Connected,
    Disconnected
}

public class SessionPlayerViewModel
{
    public string Name { get; }
    public ConnectionStatus ConnectionStatus { get; }
    public string Time { get; }
    public string Progress { get; }
}