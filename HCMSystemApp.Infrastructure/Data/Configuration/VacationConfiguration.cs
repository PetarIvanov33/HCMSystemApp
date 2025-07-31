using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCMSystemApp.Infrastructure.Data.Configuration
{
    internal class VacationConfiguration : IEntityTypeConfiguration<Vacation>
    {
        public void Configure(EntityTypeBuilder<Vacation> builder)
        {
            var vacations = new List<Vacation>
            {
                new Vacation
                {
                    Id = 1,
                    StartDate = new DateTime(2025, 5, 10),
                    EndDate = new DateTime(2025, 5, 17),
                    Reason = "Почивка в Гърция",
                    IsApproved = true,
                    IsReviewed = true,
                    UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" // employee 1
                },
                new Vacation
                {
                    Id = 2,
                    StartDate = new DateTime(2025, 6, 3),
                    EndDate = new DateTime(2025, 6, 10),
                    Reason = "Семейна почивка",
                    IsApproved = true,
                    IsReviewed = true,
                    UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" // employee 2
                },
                new Vacation
                {
                    Id = 3,
                    StartDate = new DateTime(2025, 6, 20),
                    EndDate = new DateTime(2025, 6, 25),
                    Reason = "Лични ангажименти",
                    IsApproved = true,
                    IsReviewed = true,
                    UserId = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" // manager
                }
                ,
                new Vacation
                {
                Id = 4,
                StartDate = new DateTime(2025, 8, 12),
                EndDate = new DateTime(2025, 8, 19),
                Reason = "Екскурзия до Италия",
                IsApproved = true,
                IsReviewed = true,
                UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" // employee 1
                },
                new Vacation
                {
                Id = 5,
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 9, 8),
                Reason = "Почивка в планината",
                IsApproved = true,
                IsReviewed = true,
                UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" // employee 2
                },
                new Vacation
                {
                Id = 6,
                StartDate = new DateTime(2025, 10, 15),
                EndDate = new DateTime(2025, 10, 20),
                Reason = "Конференция и кратка отпуска",
                IsApproved = true,
                IsReviewed = true,
                UserId = "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" // manager
                }

            };

            builder.HasData(vacations);
        }
    }
}
