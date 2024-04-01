using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SessionController : Controller
{
    public async Task<IActionResult> Index(string projectId, string sessionId)
    {
        Session session = await ServerConnection.ServerConnection.GetSession(projectId, sessionId);
        
        SessionViewModel sessionViewModel = new SessionViewModel(session.s_id, session.s_name, session.s_students);
        return View(sessionViewModel);
    }
}