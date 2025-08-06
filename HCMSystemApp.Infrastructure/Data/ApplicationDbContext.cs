using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HCMSystemApp.Infrastructure.Data.Configuration;
using System.Reflection.Emit;

namespace HCMSystemApp.Infrastructure.Data
{
    /// <summary>
    /// Application database context for the HCM System.
    /// Inherits from <see cref="IdentityDbContext{TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken}"/>
    /// to integrate ASP.NET Core Identity with custom User and Role entities.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<
      User,
      Role,
      string,
      IdentityUserClaim<string>,
      UserRole,
      IdentityUserLogin<string>,
      IdentityRoleClaim<string>,
      IdentityUserToken<string>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions"/> to configure the context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configures entity relationships, constraints, and seeds initial data.
        /// </summary>
        /// <param name="builder">The model builder used to configure entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserRoleConfiguration());

            //builder.Entity<Payroll>()
            //.HasOne(p => p.Salary)
            //.WithMany()
            //.HasForeignKey(p => p.SalaryId)
            //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull); // restrict changed to setNull

            // Apply Fluent API configurations
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new ManagerConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new SalaryConfiguration());
            builder.ApplyConfiguration(new PayrollConfiguration());
            builder.ApplyConfiguration(new VacationConfiguration());
        }

        // ✅ DbSets
        /// <summary>
        /// Gets or sets the employees table.
        /// </summary>
        public DbSet<Employee> Employees => Set<Employee>();

        /// <summary>
        /// Gets or sets the managers table.
        /// </summary>
        public DbSet<Manager> Managers => Set<Manager>();

        /// <summary>
        /// Gets or sets the departments table.
        /// </summary>
        public DbSet<Department> Departments => Set<Department>();

        /// <summary>
        /// Gets or sets the payrolls table.
        /// </summary>
        public DbSet<Payroll> Payrolls => Set<Payroll>();

        /// <summary>
        /// Gets or sets the vacations table.
        /// </summary>
        public DbSet<Vacation> Vacations => Set<Vacation>();

        /// <summary>
        /// Gets or sets the user roles mapping table.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
