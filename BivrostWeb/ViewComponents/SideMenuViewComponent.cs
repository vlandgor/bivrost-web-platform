using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.ViewComponents;

public class SideMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        string? email = HttpContext.Session.GetString("Email");

        // TODO: fix this cookie shit!!!!!
        if (email == null)
        {
            email = "q";
        }
        
        SideMenuViewModel model = new SideMenuViewModel()
        {
            Projects = await AwsConnectionService.GetProjectsList(email)
        };
        
        return View(model);
    }
}