using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class SessionController : Controller
{
    // GET
    public IActionResult Index(int projectID)
    {
        return View();
    }
}