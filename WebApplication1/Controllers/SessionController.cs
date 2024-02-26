using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bootstrapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SessionController : Controller
{
    private Project? activeProject;
    
    public IActionResult Index(string projectId)
    {
        activeProject = ProjectRepository.GetProjectById(projectId);
        Session session = new Session(projectId);
        return View(session);
    }
}