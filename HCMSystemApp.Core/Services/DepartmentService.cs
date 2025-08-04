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
            return await repo.AllReadonly<Department>()
                .Include(d => d.Manager)
                .ThenInclude(d => d.User)
                .Include(d => d.Employees)
                .Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ManagerId = d.Manager.Id,
                    ManagerName = d.Manager.User.FirstName + " " + d.Manager.User.LastName,
                    UserIdOfManager = d.Manager.User.Id,
                    EmployeeCount = d.Employees.Count()
                }).ToListAsync();

        }

        public async Task<DepartmentViewModel?> GetDepartmentByManagerUserIdAsync(string managerUserId)
        {
            return await repo.All<Department>()
                .Include(d => d.Manager)
                .ThenInclude(d => d.User)
                .Include(d => d.Employees)
                .Where(d => d.Manager.UserId == managerUserId)
                .Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ManagerId = d.Manager.Id,
                    ManagerName = d.Manager.User.FirstName + " " + d.Manager.User.LastName,
                    UserIdOfManager = d.Manager.User.Id,
                    EmployeeCount = d.Employees.Count()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DisplayedEmployeeModel>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            return await repo.All<Employee>()
                .Include(e => e.Department)
                .ThenInclude(e => e.Manager)
                .ThenInclude(e => e.User)
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
                    ManagerIdOfEmployee = e.Department.Manager.User.Id,
                    SalaryAmount = e.User.Salary.Amount
                })
                .ToListAsync();
        }

    }
}
