using BivrostWeb.Models;
using BivrostWeb.Models.ViewModels;
using BivrostWeb.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace BivrostWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(ILogger<ProjectController> logger, Server.Server server) : Controller
{
    [HttpGet("{projectId}")]
    public async Task<IActionResult> Project(string projectId)
    {
        Project project = await AwsConnectionService.GetProject(projectId);
        
        Console.WriteLine(project.FullName);
        
        ProjectViewModel projectViewModel = new ProjectViewModel(project);
        
        return View(projectViewModel);
    }
    
    [HttpGet("{projectId}/Session/{sessionId}")]
    public async Task<IActionResult> Session(string projectId, string sessionId)
    {
        Project project = await AwsConnectionService.GetProject(projectId);
        Session session = await AwsConnectionService.GetSession(projectId, sessionId);
        
        SessionViewModel sessionViewModel = new SessionViewModel(project, session);
        return View(sessionViewModel);
    }

    [HttpGet("{projectId}/Session/{sessionId}/Student/{studentId}")]
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
    
    [HttpPost("{projectId}/InviteUser")]
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
    
    [HttpGet("{projectId}/Settings")]
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

    [HttpPost("{projectId}/CreateSession")]
    public async Task<IActionResult> CreateSession(string projectId, string sessionId, string sessionName)
    {
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        
        await AwsConnectionService.AddNewSession(projectId, new Session(sessionId, sessionName, true, currentDate,new List<Student>(), 0));
        await server.AddSession(sessionId, sessionName);
        
        return RedirectToAction("Project", new { projectId });
    }
    
    [HttpPost("{projectId}/Session/{sessionId}/AddStudent")]
    public async Task<IActionResult> AddStudent(string projectId, string sessionId, string studentId,  string studentName)
    {
        await AwsConnectionService.AddNewStudent(projectId, sessionId, new Student(sessionId, studentName, 0, false));
        await server.AddStudent(sessionId, studentId, studentName);
        
        return RedirectToAction("Session", new { projectId, sessionId });
    }
    
    [HttpPost("{projectId}/Session/{sessionId}/RemoveStudents")]
    public async Task<IActionResult> RemoveStudents(string projectId, string sessionId, string[] studentsId)
    {
        await AwsConnectionService.RemoveStudents(projectId, sessionId, studentsId);
        await server.RemoveStudents(sessionId, studentsId);
        
        return RedirectToAction("Session", new { projectId, sessionId });
    }
    
    [HttpPost("{projectId}/Session/{sessionId}/AddStudentAjax")]
    public async Task<IActionResult> AddStudentAjax(string projectId, string sessionId, string studentId, string studentName)
    {
        var student = new Student(studentId, studentName, 0, false);
        await AwsConnectionService.AddNewStudent(projectId, sessionId, student);
        await server.AddStudent(sessionId, studentId, studentName);

        return Json(new { success = true, student });
    }
    
    [HttpGet("sessions")]
    public IActionResult GetActiveSessions()
    {
        List<Server.Models.Session> sessions = server.GetActiveSessions();
        
        return Ok(sessions);
    }
    
    [HttpGet("{sessionId}/students")]
    public IActionResult GetStudentsInSession(string sessionId)
    {
        List<Server.Models.Student> students = server.GetSession(sessionId).GetStudents();
        
        return Ok(students);
    }
}