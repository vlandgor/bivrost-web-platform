using BivrostWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BivrostWeb.ViewComponents;

public class ProjectMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ProjectMenuViewModel projectViewModel)
    {
        return View(projectViewModel);
    }
}