using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCRM.DataAccess.AdminPanel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register DatabaseHelper as a service
builder.Services.AddSingleton<User_DataAccess>();
// Register Project_DataAccess
builder.Services.AddScoped<Project_DataAccess>(provider =>
    new Project_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<Service_DataAccess>(provider =>
    new Service_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<Industry_DataAccess>(provider =>
    new Industry_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<Contact_DataAccess>(provider =>
    new Contact_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<Company_DataAccess>(provider =>
    new Company_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<Privacy_DataAccess>(provider =>
    new Privacy_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<Slider_DataAccess>(provider =>
    new Slider_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<Technology_DataAccess>(provider =>
    new Technology_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
builder.Services.AddScoped<WebsiteConfig_DataAccess>(provider =>
    new WebsiteConfig_DataAccess(builder.Configuration.GetConnectionString("WeblinkDBConnection")));
// Add Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();