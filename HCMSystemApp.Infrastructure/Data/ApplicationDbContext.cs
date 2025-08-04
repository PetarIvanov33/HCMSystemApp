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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserRoleConfiguration());

            builder.Entity<Payroll>()
            .HasOne(p => p.Salary)
            .WithMany()
            .HasForeignKey(p => p.SalaryId)
            .OnDelete(DeleteBehavior.Restrict);

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

            //Apply Fluent API

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
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Manager> Managers => Set<Manager>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Payroll> Payrolls => Set<Payroll>();
        public DbSet<Vacation> Vacations => Set<Vacation>();
        public DbSet<UserRole> UserRoles { get; set; }


    }
}
