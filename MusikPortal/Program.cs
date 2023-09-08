using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ������ NuGet Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
// ������ NuGet Package Microsoft.EntityFrameworkCore.SqlServer �� ������� ������ ��� ������ � ��
// ������ NuGet Package AutoMapper �� ������ ������ ��� �������������� ������� ������� � ������ DTO
// ������ NuGet Package BCrypt.Net-Next �� ������ ������ ��� �����������
builder.Services.AddMusicPortalContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IStyleService, StyleService>();
builder.Services.AddTransient<ISongService, SongService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISaltService, SaltService>();

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

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
