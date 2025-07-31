using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCMSystemApp.Infrastructure.Data.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Position = "Junior .Net Developer",
                    UserId = "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", // employee1@example.com
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    Position = "Sinior full stack Developer",
                    UserId = "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", // employee2@example.com
                    DepartmentId = 1
                }
            };

            builder.HasData(employees);
        }
    }
}
