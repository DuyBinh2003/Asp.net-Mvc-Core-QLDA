using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DoAn.MigrationSeeder;
using DoAn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<CContext>(options => options.UseMySQL(conn));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Logging.AddConsole();
// Add session services
builder.Services.AddDistributedMemoryCache(); // Use an appropriate distributed cache in production
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Giữ nguyên thời gian timeout
    options.Cookie.HttpOnly = true;                 // Vẫn giữ HttpOnly để bảo mật
    options.Cookie.IsEssential = true;              // Vẫn giữ IsEssential
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Định nghĩa đường dẫn đăng nhập
        options.LogoutPath = "/Account/Logout"; // Định nghĩa đường dẫn đăng xuất
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn cookie
        options.SlidingExpiration = true; // Kéo dài phiên làm việc khi người dùng vẫn đang hoạt động
    });
/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("http://localhost:7147/") // Chỉ cho phép domain này
               .AllowAnyHeader()
               .AllowAnyMethod());
});

builder.Services.AddControllers();
*/
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
app.UseSession();
app.UseNotyf();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("X-Frame-Options");
    context.Response.Headers["X-Frame-Options"] = "ALLOWALL"; // Cho phép nhúng iframe từ mọi nguồn
    await next();
});
// Add session middleware
app.UseSession();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CContext>();
        CContextSeeder.Seed(context);
    }
    catch (Exception ex)
    {
        // Log the error or handle it as needed
    }
}

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "customer",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();