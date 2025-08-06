using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Services;
using HCMSystemApp.Infrastructure.Data;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Web
{
    /// <summary>
    /// Entry point of the application, responsible for configuring services, middleware, and application startup.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that initializes the web application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Conecting with database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register repositories and application services
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IVacationService, VacationService>();
            builder.Services.AddScoped<IPayrollService, PayrollService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Identity with custom User & Role
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            // MVC + Razor
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages(); //!!!

            var app = builder.Build();

            // Middleware configuration
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Area route configuration
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            // Default route configuration
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
