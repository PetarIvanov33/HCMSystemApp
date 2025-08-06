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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Core.Services
{
    /// <summary>
    /// Service for managing departments, managers, and related operations.
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository repo;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentService"/> class.
        /// </summary>
        /// <param name="_repo">Repository instance for data operations.</param>
        /// <param name="_userManager">User manager for handling identity operations.</param>
        public DepartmentService(IRepository _repo, UserManager<User> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        /// <summary>
        /// Gets all departments including their manager and employee count.
        /// </summary>
        /// <returns>A collection of <see cref="DepartmentViewModel"/>.</returns>
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

        /// <summary>
        /// Gets all departments for dropdown/select purposes.
        /// </summary>
        /// <returns>A collection of <see cref="DepartmentDTO"/>.</returns>
        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsForSelect()
        {
            return await repo.AllReadonly<Department>()
                .Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToListAsync();
        }

        /// <summary>
        /// Gets a department by the manager's user ID.
        /// </summary>
        /// <param name="managerUserId">The manager's user ID.</param>
        /// <returns>A <see cref="DepartmentViewModel"/> or null if not found.</returns>
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

        /// <summary>
        /// Gets employees by department ID.
        /// </summary>
        /// <param name="departmentId">The department ID.</param>
        /// <returns>A collection of <see cref="DisplayedEmployeeModel"/>.</returns>
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

        /// <summary>
        /// Updates the name of a department.
        /// </summary>
        /// <param name="departmentId">The department ID.</param>
        /// <param name="newName">The new name of the department.</param>
        /// <returns>True if update was successful, otherwise false.</returns>
        public async Task<bool> UpdateDepartmentNameAsync(int departmentId, string newName)
        {
            var department = await repo.GetByIdAsync<Department>(departmentId);
            if (department == null)
            {
                return false;
            }

            department.Name = newName;
            await repo.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Creates a new department and assigns a new manager to it.
        /// </summary>
        /// <param name="model">Model containing manager and department data.</param>
        /// <returns>True if creation was successful, otherwise false.</returns>
        /// <exception cref="ArgumentException">Thrown when role is invalid.</exception>
        public async Task<bool> CreateDepartmentWithManagerAsync(AddManagerAndDepartmentModel model)
        {
            var managerUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Age = model.Age,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsVerified = true,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(managerUser, model.Password);
            if (!result.Succeeded)
                return false;

            var role = await repo.All<Role>().FirstOrDefaultAsync(r => r.Name == "Manager");
            if (role == null)
            {
                throw new ArgumentException("Invalid role name");
            }

            var userRole = new UserRole
            {
                UserId = managerUser.Id,
                RoleId = role.Id
            };
            await repo.AddAsync(userRole);
            await repo.SaveChangesAsync();

            var manager = new Manager
            {
                UserId = managerUser.Id,
            };

            await repo.AddAsync(manager);
            await repo.SaveChangesAsync();

            var department = new Department
            {
                Name = model.DepartmentName,
                ManagerId = manager.Id
            };

            await repo.AddAsync(department);
            await repo.SaveChangesAsync();

            var salary = new Salary
            {
                UserId = managerUser.Id,
                Amount = model.SalaryAmount
            };

            await repo.AddAsync(salary);
            await repo.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Deletes a manager and their department.
        /// Also unassigns employees from the department and deletes related data.
        /// </summary>
        /// <param name="managerId">The user ID of the manager.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> DeleteManagerAndDepartmentAsync(string managerId)
        {
            var manager = await repo
                .All<Manager>()
                .Include(m => m.Department)
                .FirstOrDefaultAsync(m => m.UserId == managerId);

            if (manager == null)
                return false;

            var employees = await repo
                .All<Employee>()
                .Where(e => e.DepartmentId == manager.Department.Id)
                .ToListAsync();

            foreach (var emp in employees)
            {
                emp.DepartmentId = null;
            }

            if (manager.Department != null)
            {
                repo.Delete(manager.Department);
            }

            var payrolls = await repo
                .All<Payroll>()
                .Where(p => p.UserId == managerId)
                .ToListAsync();

            foreach (var payroll in payrolls)
            {
                repo.Delete(payroll);
            }

            var salary = await repo
                .All<Salary>()
                .FirstOrDefaultAsync(s => s.UserId == managerId);

            if (salary != null)
            {
                repo.Delete(salary);
            }

            repo.Delete(manager);

            var userRoles = await repo
                .All<UserRole>()
                .Where(ur => ur.UserId == managerId)
                .ToListAsync();

            foreach (var role in userRoles)
            {
                repo.Delete(role);
            }

            var user = await repo
                .All<User>()
                .FirstOrDefaultAsync(u => u.Id == managerId);

            if (user != null)
            {
                repo.Delete(user);
            }

            await repo.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Gets the manager user ID for a given employee.
        /// </summary>
        /// <param name="employeeUserId">The user ID of the employee.</param>
        /// <returns>The manager's user ID, or null if not found.</returns>
        public async Task<string?> GetEmployeeManager(string employeeUserId)
        {
            var manager = await repo.AllReadonly<Employee>()
                .Include(e => e.Department)
                .ThenInclude(e => e.Manager)
                .FirstOrDefaultAsync(e => e.UserId == employeeUserId);
            if (manager != null)
                return manager?.Department?.Manager.UserId;
            else
                return null;
        }
    }
}
