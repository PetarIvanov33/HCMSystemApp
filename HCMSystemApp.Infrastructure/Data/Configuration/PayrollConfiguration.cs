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
                    GrossAmount = 5500.00m,
                    UserId = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6", // manager
                },
                new Payroll
                {
                    Id = 2,
                    Period = "01-2025",
                    IssuedOn = new DateTime(2025, 1, 31),
                    Bonus = 200.00m,
                    TaxAmount = 480.00m,
                    NetAmount = 2920.00m,
                    GrossAmount = 3200.00m,
                    UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", // employee 1
                },
                new Payroll
                {
                    Id = 3,
                    Period = "01-2025",
                    IssuedOn = new DateTime(2025, 1, 31),
                    Bonus = 250.00m,
                    TaxAmount = 510.00m,
                    NetAmount = 3140.00m,
                    GrossAmount = 3400.00m,
                    UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", // employee 2
                },
                new Payroll
                {
                    Id = 4,
                    Period = "02-2025",
                    IssuedOn = new DateTime(2025, 2, 28),
                    Bonus = 300.00m,
                    TaxAmount = 780.00m,
                    NetAmount = 4720.00m,
                    GrossAmount = 5500.00m,
                    UserId = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6", // manager
                },
                new Payroll
                {
                    Id = 5,
                    Period = "02-2025",
                    IssuedOn = new DateTime(2025, 2, 28),
                    Bonus = 150.00m,
                    TaxAmount = 460.00m,
                    NetAmount = 2890.00m,
                    GrossAmount = 3200.00m,
                    UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", // employee 1
                },
                new Payroll
                {
                    Id = 6,
                    Period = "02-2025",
                    IssuedOn = new DateTime(2025, 2, 28),
                    Bonus = 300.00m,
                    TaxAmount = 530.00m,
                    NetAmount = 3170.00m,
                    GrossAmount = 3400.00m,
                    UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", // employee 2
                }

            };

            builder.HasData(payrolls);
        }
    }
}
