using WebApplication1.Models;

namespace BivrostWeb.Models.ViewModels;

public class SettingsViewModel(Project project, List<User> users)
{
    public Project Project { get; } = project;
    public List<User> Users { get; } = users;
}