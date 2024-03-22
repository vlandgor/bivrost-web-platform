using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProjectController : Controller
{
    // GET
    public IActionResult Index()
    {
        List<Session> sessions = new List<Session>
        {
            new Session("239832nuf", "Session 0", "23.03.2322", 9, 34),
            new Session("bvy384b", "Session 1", "31.12.1111", 17, 67)
        };
        
        ProjectViewModel projectViewModel = new ProjectViewModel("rwrwewefw", "Project", sessions);
        return View(projectViewModel);
    }
}