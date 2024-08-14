using BivrostWeb.Hub;
using BivrostWeb.Server;
using BivrostWeb.Services;
using Microsoft.AspNetCore.SignalR;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSingleton<WebSocketService>();
builder.Services.AddSingleton<Server>(sp =>
{
    var webSocketService = sp.GetRequiredService<WebSocketService>();
    var hubContext = sp.GetRequiredService<IHubContext<SessionHub>>();
    return new Server(webSocketService, hubContext);
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

WebSocketService webSocketService = app.Services.GetRequiredService<WebSocketService>();
app.MapHub<SessionHub>("/sessionhub");

app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        await webSocketService.HandleWebSocketAsync(context);
    }
    
    await next(context);
});

app.Run();