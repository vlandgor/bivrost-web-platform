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

        List<Student> students = new List<Student>
        {
            new Student { Name = "Vlad", ConnectionStatus = ConnectionStatus.Connected, Duration = "23 minutes", Progress = "15%" },
            new Student { Name = "Lyosha", ConnectionStatus = ConnectionStatus.Disconnected, Duration = "43 minutes", Progress = "64%" }
        };
        
        Session session = new Session(projectId, students);
        return View(session);
    }
}