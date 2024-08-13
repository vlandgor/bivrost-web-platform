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
app.UseHttpsRedirection();
app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<SessionHub>("/sessionhub");

app.MapGet("/messages", async context =>
{
    var webSocketService = app.Services.GetRequiredService<WebSocketService>();
    var messages = webSocketService.GetMessages();
    var json = System.Text.Json.JsonSerializer.Serialize(messages);
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(json);
});

app.Use(async (context, next) =>
{
    var webSocketService = context.RequestServices.GetRequiredService<WebSocketService>();
    await webSocketService.HandleWebSocketAsync(context);
    await next(context);
});

app.Run();