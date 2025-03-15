using WCRM.DataAccess.AdminPanel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<Service_DataAccess>(provider =>
    new Service_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "WCRM", "wwwroot", "uploads")),
//    RequestPath = "/uploads"
//});

app.UseRouting();

app.UseAuthorization();

// Set custom path for appsettings.json
builder.Configuration
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "CustomConfig"))
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
