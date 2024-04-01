namespace WebApplication1.Models;

public class User
{
    public string Id { get; set; }
    public string Username { get;  set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Projects_Id { get; set; }
}