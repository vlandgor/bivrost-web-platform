using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bootstrapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProjectController : Controller
{
    private Project? activeProject;
    
    public async Task<IActionResult> Index(string projectId)
    {
        //activeProject = ProjectRepository.GetProjectById(projectId);
        List<Session> sessions = await ServerConnection.ServerConnection.GetSessionsList(projectId);

        ProjectViewModel projectViewModel = new ProjectViewModel("p2", "Test name", sessions);
        return View(projectViewModel);
    }

    public IActionResult CreateSession(string sessionId, string sessionName)
    {
        Console.Write(sessionName);

        if (activeProject != null)
        {
            return RedirectToAction("Index", new { projectId = "p2" });
        }
        
        return RedirectToAction("Index", new { projectId = "p1" });
    }
}