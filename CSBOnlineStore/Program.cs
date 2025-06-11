using Microsoft.EntityFrameworkCore;
using CSBOnlineStore.DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<CSBContext>(options =>
    options.UseLazyLoadingProxies()
    .UseNpgsql(connectionString));


var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
