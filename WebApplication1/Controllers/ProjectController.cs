using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bootstrapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProjectController : Controller
{
    private Project? activeProject;
    
    public IActionResult Index(string projectId)
    {
        activeProject = ProjectRepository.GetProjectById(projectId);
        
        ProjectViewModel projectViewModel = new ProjectViewModel("rwrwewefw", "Project", activeProject.Sessions);
        return View(projectViewModel);
    }
}