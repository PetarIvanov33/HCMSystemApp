using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Infrastructure.Data.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(CreateRoles());
        }

        private List<Role> CreateRoles()
        {
            return new List<Role>
            {
                new Role
                {
                    Id = "b4656095-c561-4bfa-a5ad-08f7678af1bf",
                    Name = "HRAdmin",
                    NormalizedName = "HRADMIN"
                },
                new Role
                {
                    Id = "a2196e3c-e72a-4778-994f-36c85380e060",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new Role
                {
                    Id = "9b325984-c63f-4dec-a00b-eeaab3d34035",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                }
            };
        }
    }
}
