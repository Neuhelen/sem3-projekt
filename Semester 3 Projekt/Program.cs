using Semester_3_Projekt.controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Changed from AddRazorPages() to AddControllersWithViews()

// Cookie authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Redirect to login page if not authenticated
    });
var connectionstring = builder.Configuration.GetConnectionString("LocalBeerDBConnectionString");
builder.Services.AddDbContext<BeerDBConn>(options =>
options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    // defalt route to login page
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
