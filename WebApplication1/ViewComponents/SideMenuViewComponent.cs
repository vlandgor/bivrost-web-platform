using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bootstrapper;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SideMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<Project> projects = await ServerConnection.ServerConnection.GetProjectsList();
        return View(projects);
    }
}