using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCMSystemApp.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Role)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ➕ Seed data за ролевите връзки:
            builder.HasData(
                new UserRole
                {
                    UserId = "8d04dce2-969a-435d-bba4-df3f325983dc", // Admin
                    RoleId = "b4656095-c561-4bfa-a5ad-08f7678af1bf"  // HRAdmin
                },
                new UserRole
                {
                    UserId = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6", // Manager
                    RoleId = "a2196e3c-e72a-4778-994f-36c85380e060"
                },
                new UserRole
                {
                    UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", // Employee 1
                    RoleId = "9b325984-c63f-4dec-a00b-eeaab3d34035"
                },
                new UserRole
                {
                    UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", // Employee 2
                    RoleId = "9b325984-c63f-4dec-a00b-eeaab3d34035"
                }
            );
        }
    }
}
