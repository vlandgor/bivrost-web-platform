using BivrostWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BivrostWeb.ViewComponents;

public class ProjectMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ProjectViewModel projectViewModel)
    {
        return View(projectViewModel);
    }
}