using System.Drawing;
using BivrostWeb.Models;

namespace WebApplication1.Models;

public class User(string id, string username, string email, string password, List<string> projectsId, Role role, string accountColor)
{
    public string Id { get; } = id;
    public string Username { get; } = username;
    public string Email { get; } = email;
    public string Password { get; } = password;
    public List<string> Projects_Id { get; } = projectsId;
    public Role Role { get; } = role;
    public string AccountColor { get; } = accountColor;
}