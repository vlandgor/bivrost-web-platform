using BivrostWeb.Models.ViewModels;
using BivrostWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace BivrostWeb.Controllers;

[Authorize]
public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        string? email = HttpContext.Session.GetString("Email");

        // TODO: fix this cookie shit!!!!!
        if (email == null)
        {
            email = "q";
        }

        List<Project> projects = await AwsConnectionService.GetProjectsList(email);
        HomeViewModel model = new HomeViewModel(projects);
        
        return View(model);
    }

    public async Task<IActionResult> CreateProject(string projectId, string fullName, string shortName)
    {
        Project project = new Project(projectId, fullName, shortName);

        await AwsConnectionService.AddNewProject(project);
        
        return RedirectToAction("Project", "Project", new { projectId = projectId });
    }
}