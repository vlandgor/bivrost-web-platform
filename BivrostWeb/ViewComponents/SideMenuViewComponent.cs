using BivrostWeb.Models.ViewModels;
using BivrostWeb.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents;

public class SideMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        string? email = HttpContext.Session.GetString("Email");
        string? accountColor = HttpContext.Session.GetString("AccountColor");

        Console.WriteLine(email);
        
        // TODO: fix this cookie shit!!!!!
        if (email == null)
        {
            email = "q";
        }

        if (accountColor == null)
        {
            accountColor = "#000000";
        }
        
        List<Project> projects = await AwsConnectionService.GetProjectsList(email);

        SideMenuViewModel model = new SideMenuViewModel(projects, accountColor);
        
        return View(model);
    }
}