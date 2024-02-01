using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExtensins();
//builder.Services.AddDbContext<GamzeBlogContext>();
builder.Services.AddDbContext<GamzeBlogContext>(context => context.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );
//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{controller=Home}/{action=Index}/{area?}");
//app.MapControllerRoute(
//    name: "area",
//    pattern: "{controller=Home}/{action=Index}/{id}/{area=Admin}");

app.Run();
