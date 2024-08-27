using BivrostWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BivrostWeb.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        string? accountColor = HttpContext.Session.GetString("AccountColor");

        if (accountColor == null)
        {
            accountColor = "#000000";
        }

        HeaderViewModel model = new HeaderViewModel(accountColor);
        
        return View(model);
    }
}