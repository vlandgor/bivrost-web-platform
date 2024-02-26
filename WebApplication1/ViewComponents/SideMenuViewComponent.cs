using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SideMenuViewComponent: ViewComponent
{
    private readonly List<Project> projects = new List<Project>
    {
        new Project { Id = 1, FullName = "Project 1", ShortName = "P1" },
        new Project { Id = 2, FullName = "Project 2", ShortName = "P2" },
        new Project { Id = 3, FullName = "Project 3", ShortName = "P3" }
    };
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(projects);
    }
}