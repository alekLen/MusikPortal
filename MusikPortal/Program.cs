using Microsoft.EntityFrameworkCore;
using MusikPortal.Models;
using MusikPortal.Repository;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<MusikPortalContext>(options => options.UseSqlServer(connection));
// качаем NuGet Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
// качаем NuGet Package Microsoft.EntityFrameworkCore.SqlServer
// качаем NuGet Package BCrypt.Net-Next


// добавляем сервис для динамического создания вьюшек
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Длительность сеанса (тайм-аут завершения сеанса)
    options.Cookie.Name = "Session"; // Каждая сессия имеет свой идентификатор, который сохраняется в куках.

});
// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository, MusikPortalRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
