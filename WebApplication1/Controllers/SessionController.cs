using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bootstrapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SessionController : Controller
{
    private Session? session;
    
    public IActionResult Index(string sessionId)
    {
        //session = ProjectRepository.GetSessionById(sessionId);

        List<SessionStudentViewModel> students = new List<SessionStudentViewModel>
        {
            new SessionStudentViewModel { Name = "Vlad", ConnectionStatus = true, Duration = "23 minutes", Progress = "15%" },
            new SessionStudentViewModel { Name = "Lyosha", ConnectionStatus = false, Duration = "43 minutes", Progress = "64%" }
        };
        
        SessionViewModel sessionViewModel = new SessionViewModel(sessionId, session.s_name, students);
        return View(sessionViewModel);
    }
}