using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Core.Models.Users;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository repo;

        public DepartmentService(IRepository _repo)
        {
            repo = _repo;

        }
        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartments()
        {
            return await repo.AllReadonly<Department>().
                Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                }).ToListAsync();

        }

        public async Task<DepartmentViewModel?> GetDepartmentByManagerUserIdAsync(string managerUserId)
        {
            return await repo.All<Department>()
                .Where(d => d.Manager.UserId == managerUserId)
                .Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DisplayedEmployeeModel>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            return await repo.All<Employee>()
                .Where(e => e.DepartmentId == departmentId)
                .Select(e => new DisplayedEmployeeModel
                {
                    UserId = e.User.Id,
                    FirstName = e.User.FirstName,
                    LastName = e.User.LastName,
                    UserName = e.User.UserName,
                    Age = e.User.Age,
                    Email = e.User.Email,
                    PhoneNumber = e.User.PhoneNumber,
                    Position = e.Position,
                    SalaryAmount = e.User.Salary.Amount
                })
                .ToListAsync();
        }

    }
}
