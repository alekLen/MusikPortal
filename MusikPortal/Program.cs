using Microsoft.EntityFrameworkCore;
using MusikPortal.Models;
using MusikPortal.Repository;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<MusikPortalContext>(options => options.UseSqlServer(connection));
// ������ NuGet Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
// ������ NuGet Package Microsoft.EntityFrameworkCore.SqlServer
// ������ NuGet Package BCrypt.Net-Next


// ��������� ������ ��� ������������� �������� ������
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // ������������ ������ (����-��� ���������� ������)
    options.Cookie.Name = "Session"; // ������ ������ ����� ���� �������������, ������� ����������� � �����.

});
// ��������� ������� MVC
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository, MusikPortalRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
