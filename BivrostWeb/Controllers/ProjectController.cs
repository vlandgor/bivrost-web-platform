using BivrostWeb.Models;
using BivrostWeb.Models.ViewModels;
using BivrostWeb.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace BivrostWeb.Controllers;

public class ProjectController(ILogger<ProjectController> logger, Server.Server server) : Controller
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
    
    public async Task<IActionResult> InviteUser(string email, string projectId)
    {
        await AwsConnectionService.SendUserInvite(email, projectId);
        if (email == "rt")
        {
            return RedirectToAction("Project", new { projectId });
        }
        else
        {
            return null;
        }
    }
    
    public async Task<IActionResult> Settings(string projectId)
    {
        Project project = await AwsConnectionService.GetProject(projectId);

        List<User> users = new List<User>()
        {
            new User("id", "Back Black", "jack.black@gmail.com", "8tfhikbvjhd", null, Role.Leader, "#3357FF"),
            new User("girbh", "Barbara Cell", "h@gmail.com", "bdgsbf", null, Role.Developer, "#33FFF6")
        };
        
        SettingsViewModel settingsViewModel = new SettingsViewModel(project, users);
        return View(settingsViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(string projectId, string sessionId, string sessionName)
    {
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        
        await AwsConnectionService.AddNewSession(projectId, new Session(sessionId, sessionName, true, currentDate,new List<Student>(), 0));
        await server.AddSession(sessionId, sessionName);
        
        return RedirectToAction("Project", new { projectId });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddStudent(string projectId, string sessionId, string studentId,  string studentName)
    {
        await AwsConnectionService.AddNewStudent(projectId, sessionId, new Student(sessionId, studentName, 0, false));
        await server.AddStudent(sessionId, studentId, studentName);
        
        return RedirectToAction("Session", new { projectId, sessionId });
    }
}