using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Services;
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

            // Conecting with database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService,  RoleService>();


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Identity with custom User & Role
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
