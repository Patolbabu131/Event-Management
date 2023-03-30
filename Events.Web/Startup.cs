//using Events.Business;
//using Events.Repository;
//using Events.Services;
//using Events.Web.Models;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;  

//namespace Events.Web
//{
//    public class Startup
//        {
//            public Startup(IConfiguration configuration)
//            {
//                Configuration = configuration;
//            }
//            public IConfiguration Configuration { get; set; }
//            public void ConfigureServices(IServiceCollection services)
//            {
//                services.AddRazorPages();
//                services.AddControllersWithViews();


//                services.AddTransient<IViewRenderingService, ViewRenderingService>();
//                services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
//                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//                services.ConfigureApplicationCookie(option =>
//                {
//                    option.LoginPath = "/Account/Login";
//                    option.SlidingExpiration = true;
//                    option.ExpireTimeSpan = TimeSpan.FromSeconds(30);
//                });
//                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        ClockSkew = TimeSpan.FromSeconds(30),
//                        ValidIssuer = Configuration["Jwt:Issuer"],
//                        ValidAudience = Configuration["Jwt:Issuer"],
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
//                    };
//                });

//                services.AddMvc();
//                services.AddSession();
        
//                services.AddTransient<IEventsService, EventsService>();
//                services.AddTransient<IEventsRepository, EventsRepository>();
//                services.AddDbContext<EventDbContext, EventDbContext>();
//            }

//            public void Configure(WebApplication app, IWebHostEnvironment env)
//        {
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseRouting();
//            app.UseStaticFiles();
//            app.UseAuthentication();
//            app.UseAuthorization();
//            app.UseSession();
//            app.MapControllerRoute(
//                name: "default",

//                pattern: "{controller=Account}/{action=login}/{id?}");

//            app.Run();

//        }
     
//    }
//}
