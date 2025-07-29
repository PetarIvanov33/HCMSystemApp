using HCMSystemApp.Infrastructure.Data;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ?? Свързване с база данни
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepository, Repository>();

            // ?? Debug помощ при грешки
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // ?? Регистрация на Identity с custom User и Role
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // ??? MVC + Razor
            builder.Services.AddControllersWithViews();
            //builder.Services.AddRazorPages();

            var app = builder.Build();

            // ?? Middleware конфигурация
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
