using BivrostWeb.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace BivrostWeb.Controllers;

public class ProjectController : Controller
{
    public async Task<IActionResult> Project(string projectId)
    {
        Project project = await AwsConnectionService.GetProject(projectId);
        
        ProjectViewModel projectViewModel = new ProjectViewModel(project);
        
        return View(projectViewModel);
    }
    
    public async Task<IActionResult> Session(string projectId, string sessionId)
    {
        Project project = await AwsConnectionService.GetProject(projectId);
        Session session = await AwsConnectionService.GetSession(projectId, sessionId);
        
        SessionViewModel sessionViewModel = new SessionViewModel(project, session);
        return View(sessionViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(string projectId, string sessionId, string sessionName)
    {
        await AwsConnectionService.AddNewSession(projectId, new Session(sessionId, sessionName, true, new List<Student>()));
        
        return RedirectToAction("Project", new { projectId = projectId });
    }
}