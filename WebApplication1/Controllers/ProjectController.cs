using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bootstrapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProjectController : Controller
{
    public async Task<IActionResult> Index(string projectId)
    {
        List<Session> sessions = await ServerConnection.ServerConnection.GetSessionsList(projectId);
        ProjectViewModel projectViewModel = new ProjectViewModel(projectId, sessions);
        
        return View(projectViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(string projectId, string sessionId, string sessionName)
    {
        await ServerConnection.ServerConnection.AddNewSession(projectId, new Session(sessionId, sessionName, true, new List<Student>()));
        
        return RedirectToAction("Index", new { projectId = projectId });
    }
}