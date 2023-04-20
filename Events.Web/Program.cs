using Events.Business;
using Events.Database;
using Events.Repository;
using Events.Services;
using Events.Web.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddTransient<IViewRenderingService, ViewRenderingService>();
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(600);
  
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(x =>
        {
            x.Cookie.Name = "CookieAuthenticationDefaults";
            x.ExpireTimeSpan = TimeSpan.FromMinutes(10);//For Auto Logout 
            x.LoginPath = "/Account/login";
            x.SlidingExpiration = true;
        });
    
builder.Services.AddTransient<IEventsService, EventsService>();
builder.Services.AddTransient<IEventsRepository, EventsRepository>();
builder.Services.AddDbContext<EventDbContext>();
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Account}/{action=login}/{id?}");

app.Run();

