using DatabaseModel.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GZMWebAppDbContext>(
    a => a.UseSqlServer("Server=.;Database=GZMWebAppDb;Trusted_Connection=True;Encrypt=False"));

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Perfumes}/{action=Index}/{id?}");

app.Run();
