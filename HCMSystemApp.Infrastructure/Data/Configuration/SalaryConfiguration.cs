using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCMSystemApp.Infrastructure.Data.Configuration
{
    internal class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            var salaries = new List<Salary>
            {
                new Salary
                {
                    Id = 1,
                    UserId = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6", // manager@example.com
                    Amount = 5500.00m
                },
                new Salary
                {
                    Id = 2,
                    UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", // employee1@example.com
                    Amount = 3200.00m
                },
                new Salary
                {
                    Id = 3,
                    UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", // employee2@example.com
                    Amount = 3400.00m
                }
            };

            builder.HasData(salaries);
        }
    }
}
