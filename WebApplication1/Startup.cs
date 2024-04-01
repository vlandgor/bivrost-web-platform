using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication1;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromMinutes(60 * 1);
                option.LoginPath = "/Account/Login";
                option.AccessDeniedPath = "/Account/Login";
            });

        services.AddSession(option =>
        {
            option.IdleTimeout = TimeSpan.FromMinutes(5);
            option.Cookie.HttpOnly = true;
            option.Cookie.IsEssential = true;
        });
    }
}