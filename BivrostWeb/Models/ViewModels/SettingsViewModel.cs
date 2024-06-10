using WebApplication1.Models;

namespace BivrostWeb.Models.ViewModels;

public class SettingsViewModel(Project project)
{
    public Project Project { get; } = project;
}