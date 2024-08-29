namespace BivrostWeb.Models.ViewModels;

public class ProjectMenuViewModel(string projectName)
{
    public string ProjectName { get; private set; } = projectName;
}