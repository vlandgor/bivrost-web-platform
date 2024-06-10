using WebApplication1.Models;

namespace BivrostWeb.Models.ViewModels;

public class ProjectViewModel(Project project)
{
    public Project Project { get; } = project;
}