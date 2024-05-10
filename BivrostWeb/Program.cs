using WebApplication1;
using WebApplication1.Hub;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddHostedService<TcpListenerService>();

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

// Map SignalR hubs
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<KeepAliveHub>("/keepAliveHub");
    endpoints.MapHub<StudentHub>("/studentHub");
    endpoints.MapRazorPages(); // Map Razor Pages or other endpoints as needed
});

app.Run();