using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCMSystemApp.Infrastructure.Data.Configuration
{
    internal class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            var payrolls = new List<Payroll>
            {
                new Payroll
                {
                    Id = 1,
                    Period = "01-2025",
                    IssuedOn = new DateTime(2025, 1, 31),
                    Bonus = 500.00m,
                    TaxAmount = 750.00m,
                    NetAmount = 4750.00m,
                    UserId = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6", // manager
                    SalaryId = 1
                },
                new Payroll
                {
                    Id = 2,
                    Period = "01-2025",
                    IssuedOn = new DateTime(2025, 1, 31),
                    Bonus = 200.00m,
                    TaxAmount = 480.00m,
                    NetAmount = 2920.00m,
                    UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", // employee 1
                    SalaryId = 2
                },
                new Payroll
                {
                    Id = 3,
                    Period = "01-2025",
                    IssuedOn = new DateTime(2025, 1, 31),
                    Bonus = 250.00m,
                    TaxAmount = 510.00m,
                    NetAmount = 3140.00m,
                    UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", // employee 2
                    SalaryId = 3
                }
            };

            builder.HasData(payrolls);
        }
    }
}
