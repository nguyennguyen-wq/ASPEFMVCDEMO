using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebEFMVCDemo.Data;
using WebEFMVCDemo.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebEFMVCDemoContext>(options =>
    options.UseNpgsql(builder.Configuration
    .GetConnectionString("WebEFMVCDemoContext") ?? throw new InvalidOperationException("Connection string 'WebEFMVCDemoContext' not found.")));
builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
