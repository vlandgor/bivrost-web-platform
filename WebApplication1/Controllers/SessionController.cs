using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bootstrapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SessionController : Controller
{
    private Project? activeProject;
    
    public IActionResult Index(int projectId)
    {
        activeProject = ProjectRepository.GetProjectById(projectId);

        List<SessionStudentViewModel> students = new List<SessionStudentViewModel>
        {
            new SessionStudentViewModel { Name = "Vlad", ConnectionStatus = ConnectionStatus.Connected, Duration = "23 minutes", Progress = "15%" },
            new SessionStudentViewModel { Name = "Lyosha", ConnectionStatus = ConnectionStatus.Disconnected, Duration = "43 minutes", Progress = "64%" }
        };
        
        SessionViewModel sessionViewModel = new SessionViewModel(projectId, students);
        return View(sessionViewModel);
    }
}