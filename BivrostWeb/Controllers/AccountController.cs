using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

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
            User data = await AwsConnectionService.GetUserData(model.Email);
            bool isValid = data.Email == model.Email && data.Password == model.Password;
            if (isValid)
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, data.Email) },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                
                HttpContext.Session.SetString("Email", data.Email);
                await HttpContext.Session.CommitAsync();
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
    public async Task<IActionResult> SignUp(AccountSignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            var data = new User()
            {
                Id = model.Username,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Projects_Id = new List<string>()
            };

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