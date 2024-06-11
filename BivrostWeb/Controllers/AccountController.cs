using System.Drawing;
using System.Security.Claims;
using BivrostWeb.Models;
using BivrostWeb.Models.ViewModels;
using BivrostWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace BivrostWeb.Controllers;

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
            User data = await AwsConnectionService.GetUserData(model.Email);
            bool isValid = data.Email == model.Email && data.Password == model.Password;
            if (isValid)
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, data.Email) },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                
                HttpContext.Session.SetString("Email", data.Email);
                HttpContext.Session.SetString("Role", data.Role.ToString());
                
                if (!string.IsNullOrEmpty(data.AccountColor))
                {
                    HttpContext.Session.SetString("AccountColor", data.AccountColor);
                }
                
                await HttpContext.Session.CommitAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["errorPassword"] = $"Invalid password!";
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
    public async Task<IActionResult> SignUp(AccountSignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            var data = new User(model.Username, model.Username, model.Email, model.Password, new List<string>(),
                Role.Instructor, "#3357FF");
            await AwsConnectionService.RegisterNewUser(data);
            
            TempData["successMessage"] = $"You are eligible to login, Please fill own credential's then login!";
            return RedirectToAction("Login");
        }
        else
        {
            TempData["errorMessage"] = "Empty form can't be submitted!";
            return View(model);
        }
    }
}