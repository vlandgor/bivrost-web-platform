using WebApplication1.Models;

namespace BivrostWeb.Models.ViewModels;

public class HomeViewModel(List<Project> projects)
{
    public List<Project> Projects { get; } = projects;
}