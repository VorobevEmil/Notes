using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Notes.Identity;
using Notes.Identity.Data;
using Notes.Identity.Models;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["DbConnection"];

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScropes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Notes.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Styles")),
    RequestPath = "/styles"
});
app.UseRouting();
app.UseIdentityServer();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
