using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            var users = new List<User>();

            var admin = new User
            {
                Id = "8d04dce2-969a-435d-bba4-df3f325983dc",
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                FirstName = "Admin",
                LastName = "User",
                Age = 40,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            admin.PasswordHash = hasher.HashPassword(admin, "123456!");

            var manager = new User
            {
                Id = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6",
                UserName = "manager@example.com",
                NormalizedUserName = "MANAGER@EXAMPLE.COM",
                Email = "manager@example.com",
                NormalizedEmail = "MANAGER@EXAMPLE.COM",
                FirstName = "Manager",
                LastName = "User",
                Age = 35,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            manager.PasswordHash = hasher.HashPassword(manager, "123456!");

            var employee1 = new User
            {
                Id = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9",
                UserName = "employee1@example.com",
                NormalizedUserName = "EMPLOYEE1@EXAMPLE.COM",
                Email = "employee1@example.com",
                NormalizedEmail = "EMPLOYEE1@EXAMPLE.COM",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Age = 28,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            employee1.PasswordHash = hasher.HashPassword(employee1, "123456!");

            var employee2 = new User
            {
                Id = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1",
                UserName = "employee2@example.com",
                NormalizedUserName = "EMPLOYEE2@EXAMPLE.COM",
                Email = "employee2@example.com",
                NormalizedEmail = "EMPLOYEE2@EXAMPLE.COM",
                FirstName = "Georgi",
                LastName = "Georgiev",
                Age = 30,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            employee2.PasswordHash = hasher.HashPassword(employee2, "123456!");

            users.AddRange(new[] { admin, manager, employee1, employee2 });

            builder.HasData(users);
        }
    }
}
