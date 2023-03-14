using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebMVCDemo.Data;


using WebMVCDemo.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebMVCDemoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebMVCDemoContext") ?? throw new InvalidOperationException("Connection string 'WebMVCDemoContext' not found.")));


builder.Services.AddControllersWithViews();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    CodeFile.Initialize(services);
}


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
