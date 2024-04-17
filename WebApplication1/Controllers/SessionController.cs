using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class SessionController : Controller
{
    public async Task<IActionResult> Index(string projectId, string sessionId)
    {
        Session session = await AwsConnectionService.GetSession(projectId, sessionId);
        
        SessionViewModel sessionViewModel = new SessionViewModel(projectId,session.s_id, session.s_name, session.s_students);
        return View(sessionViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStudent(string projectId, string sessionId, string studentId, string studentName)
    {
        await AwsConnectionService.AddNewStudent(sessionId, new Student(studentId, studentName, 0, false));
        
        return RedirectToAction("Index", new { projectId, sessionId });
    }
}