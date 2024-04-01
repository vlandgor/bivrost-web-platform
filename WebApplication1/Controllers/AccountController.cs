using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public async Task<IActionResult> LogIn(AccountLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User data = await ServerConnection.ServerConnection.GetUserData(model.Email);
            bool isValid = data.Email == model.Email && data.Password == model.Password;
            if (isValid)
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, data.Email) },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString("Email", data.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["errorPassword"] = "Invalid password!";
                return View(model);
            }
        }
        else
        {
            TempData["errorUsername"] = $"Username not found! {model.Email} : email";
            return View(model);
        }
    }

    public IActionResult LogOut()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("LogIn", "Account");
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