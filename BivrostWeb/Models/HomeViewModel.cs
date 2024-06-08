namespace WebApplication1.Models;

public class HomeViewModel(List<Project> projects)
{
    public List<Project> Projects { get; } = projects;
}