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

    public async Task<IActionResult> Student(string projectId, string sessionId, string studentId)
    {
        Project project = await AwsConnectionService.GetProject(projectId);
        Session session = await AwsConnectionService.GetSession(projectId, sessionId);
        
        Student student = session.s_students.FirstOrDefault(s => s.st_id == studentId);
    
        if (student == null)
        {
            // Handle the case where the student is not found
            return NotFound("Student not found");
        }

        StudentViewModel studentViewModel = new StudentViewModel(project, session, student);
        return View(studentViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(string projectId, string sessionId, string sessionName)
    {
        await AwsConnectionService.AddNewSession(projectId, new Session(sessionId, sessionName, true, new List<Student>()));
        
        return RedirectToAction("Project", new { projectId });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddStudent(string projectId, string sessionId, string studentId,  string studentName)
    {
        await AwsConnectionService.AddNewStudent(projectId, sessionId, new Student(sessionId, studentName, 0, false));
        
        return RedirectToAction("Session", new { projectId, sessionId });
    }
}