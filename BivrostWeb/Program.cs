using BivrostWeb;
using BivrostWeb.Hub;
using BivrostWeb.Server;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSingleton<Server>(serviceProvider =>
{
    var sessionHub = serviceProvider.GetRequiredService<IHubContext<SessionHub>>();
    return new Server(sessionHub);
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

Startup startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseWebSockets();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Projects}/{id?}");

app.MapHub<SessionHub>("/sessionhub");

Server server = app.Services.GetRequiredService<Server>();

app.Use(async (context, next) => await server.HandleRequestAsync(context, next));

server.Run(ServerType.Development);

app.Run();