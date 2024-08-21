using Microsoft.AspNetCore.Mvc;

namespace BivrostWeb.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}