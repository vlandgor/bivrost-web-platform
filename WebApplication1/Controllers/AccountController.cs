using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class AccountController : Controller
{
    public IActionResult LogIn()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult LogIn(AccountLoginViewModel model)
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SignUp(AccountSignUpViewModel model)
    {
        return View();
    }
}