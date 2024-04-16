using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication1;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddControllersWithViews();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // Ensure cookies are always secure
                options.Cookie.SameSite = SameSiteMode.None;  // Adjust based on your cross-site needs
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);  // Cookie expiration
                options.LoginPath = "/Account/Login";  // Redirect to login path if not authenticated
                options.AccessDeniedPath = "/Account/Login";  // Redirect when access is denied
                options.SlidingExpiration = true;  // Reset expiration time if a user is active
            });

        services.AddSession(option =>
        {
            option.IdleTimeout = TimeSpan.FromMinutes(5);
            option.Cookie.HttpOnly = true;
            option.Cookie.IsEssential = true;
        });
    }
}