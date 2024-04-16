using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SessionController : Controller
{
    public async Task<IActionResult> Index(string projectId, string sessionId)
    {
        Session session = await ServerConnection.ServerConnection.GetSession(projectId, sessionId);
        
        SessionViewModel sessionViewModel = new SessionViewModel(projectId,session.s_id, session.s_name, session.s_students);
        return View(sessionViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStudent(string projectId, string sessionId, string studentId, string studentName)
    {
        await ServerConnection.ServerConnection.AddNewStudent(sessionId, new Student(studentId, studentName, 0, false));
        
        return RedirectToAction("Index", new { projectId, sessionId });
    }
}