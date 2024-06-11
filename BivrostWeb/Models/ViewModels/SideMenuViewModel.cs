using WebApplication1.Models;

namespace BivrostWeb.Models.ViewModels;

public class SideMenuViewModel(List<Project> projects, string accountColor)
{
    public string AccountColor { get; } = accountColor;
    public List<Project> Projects { get; } = projects;
}