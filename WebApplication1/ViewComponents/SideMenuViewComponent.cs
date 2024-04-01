using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SideMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        SideMenuViewModel model = new SideMenuViewModel()
        {
            Projects = await ServerConnection.ServerConnection.GetProjectsList()
        };
        
        return View(model);
    }
}