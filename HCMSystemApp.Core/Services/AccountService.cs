using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Users;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace HCMSystemApp.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;

        public AccountService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<DisplayedUserModel>> GetAllUsersAsync()
        {
            var users = await (
                from user in repo.All<User>()
                join ur in repo.All<UserRole>() on user.Id equals ur.UserId
                join role in repo.All<Role>() on ur.RoleId equals role.Id
                where role.Name != "HRAdmin"
                select new DisplayedUserModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsVerified = user.IsVerified
                }).ToListAsync();

            return users;
        }

        public async Task<DisplayedUserModel> GetCurrentUserProfile(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);
            var model = new DisplayedUserModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsVerified = user.IsVerified,
                Role = "" 
            };
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            return model;
        }


        public async Task<DisplayedEmployeeModel?> GetCurrentEmployeeProfile(string userId)
        {
            
            var allUsers = await GetAllUsersAsync();
            var currentUser = allUsers.FirstOrDefault(u => u.UserId == userId);

            if (currentUser == null)
            {
                return null;
            }

            var employee = await repo.All<Employee>()
                                     .Include(e => e.Department)
                                     .ThenInclude(e => e.Manager)
                                     .ThenInclude(e => e.User)
                                     .FirstOrDefaultAsync(e => e.UserId == userId);

            var salary = await repo.All<Salary>()
                                   .FirstOrDefaultAsync(s => s.UserId == userId);
            if (employee.DepartmentId != null)
            {

                return new DisplayedEmployeeModel
                {
                    UserId = currentUser.UserId,
                    UserName = currentUser.UserName,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Age = currentUser.Age,
                    Email = currentUser.Email,
                    PhoneNumber = currentUser.PhoneNumber,
                    Department = employee.Department.Name ?? "N/A",
                    Position = employee?.Position ?? "N/A",
                    SalaryAmount = salary?.Amount ?? 0,
                    ManagerIdOfEmployee = employee.Department.Manager.User.Id ?? "N/A",
                    DepartmentId = employee.Department?.Id ?? 0
                };
            }
            else
            {
                return new DisplayedEmployeeModel
                {
                    UserId = currentUser.UserId,
                    UserName = currentUser.UserName,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Age = currentUser.Age,
                    Email = currentUser.Email,
                    PhoneNumber = currentUser.PhoneNumber,
                    Department = "N/A",
                    Position = employee?.Position ?? "N/A",
                    SalaryAmount = salary?.Amount ?? 0,
                    ManagerIdOfEmployee = "N/A",
                    DepartmentId = 0
                };
            }
        }

        public async Task<IEnumerable<DisplayedEmployeeModel>> GetEmployeesWithoutDepartmentAsync()
        {
            return await repo
                .All<Employee>()
                .Include(e => e.User)
                .ThenInclude(e => e.Salary)
                .Where(e => e.DepartmentId == null)
                .Select(e => new DisplayedEmployeeModel
                {
                    UserId = e.UserId,
                    FirstName = e.User.FirstName,
                    LastName = e.User.LastName,
                    UserName = e.User.UserName,
                    Email = e.User.Email,
                    PhoneNumber = e.User.PhoneNumber,
                    Age = e.User.Age,
                    Position = e.Position,
                    SalaryAmount = e.User.Salary.Amount
                })
                .ToListAsync();
        }

        public async Task<bool> AssignDepartmentToEmployeeAsync(string employeeId, int departmentId)
        {
            var employee = await repo.All<Employee>()
                .FirstOrDefaultAsync(e => e.UserId == employeeId);

            if (employee == null) return false;

            employee.DepartmentId = departmentId;
            await repo.SaveChangesAsync();
            return true;
        }


        public async Task UpdateEmployeeAsync(DisplayedEmployeeModel model)
        {
            var employee = await repo.All<Employee>()
                                     .Include(e => e.User)
                                     .FirstOrDefaultAsync(e => e.UserId == model.UserId);

            if (employee == null) throw new ArgumentException("User not found");

            employee.User.FirstName = model.FirstName;
            employee.User.LastName = model.LastName;
            employee.User.UserName = model.UserName;
            employee.User.Email = model.Email;
            employee.User.PhoneNumber = model.PhoneNumber;
            employee.User.Age = model.Age;
            employee.Position = model.Position;
            employee.DepartmentId = model.DepartmentId;

            var salary = await repo.All<Salary>().FirstOrDefaultAsync(s => s.UserId == model.UserId);
            if (salary != null)
            {
                salary.Amount = model.SalaryAmount;
            }

            await repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(string userIdToDelete, string managerUserId)
        {
            var manager = await repo.All<Manager>()
                .Include(m => m.Department)
                .FirstOrDefaultAsync(m => m.UserId == managerUserId);

            if (manager == null)
                throw new ArgumentException("Manager not found");

            var employee = await repo.All<Employee>()
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.UserId == userIdToDelete);

            if (employee == null || employee.DepartmentId != manager.Department.Id)
                return false; 

            var salary = await repo.All<Salary>()
                .FirstOrDefaultAsync(s => s.UserId == userIdToDelete);
            if (salary != null) repo.Delete(salary);

            var payrolls = await repo.All<Payroll>()
                .Where(p => p.UserId == userIdToDelete)
                .ToListAsync();
            repo.DeleteRange(payrolls);

            var userRoles = await repo.All<UserRole>()
                .Where(ur => ur.UserId == userIdToDelete)
                .ToListAsync();
            repo.DeleteRange(userRoles);

            repo.Delete(employee);

            var user = await repo.GetByIdAsync<User>(userIdToDelete);
            repo.Delete(user);

            await repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeWithoutDepartmentAsync(string employeeId)
        {
            var employee = await repo
         .All<Employee>()
         .FirstOrDefaultAsync(e => e.UserId == employeeId);

            if (employee == null)
                return false;

            var payrolls = await repo
                .All<Payroll>()
                .Where(p => p.UserId == employee.UserId)
                .ToListAsync();

            foreach (var payroll in payrolls)
            {
                repo.Delete(payroll);
            }

            var salary = await repo
                .All<Salary>()
                .FirstOrDefaultAsync(s => s.UserId == employee.UserId);

            if (salary != null)
            {
                repo.Delete(salary);
            }

            var userRoles = await repo
                .All<UserRole>()
                .Where(ur => ur.UserId == employee.UserId)
                .ToListAsync();

            foreach (var role in userRoles)
            {
                repo.Delete(role);
            }

            repo.Delete(employee);

            var user = await repo
                .All<User>()
                .FirstOrDefaultAsync(u => u.Id == employee.UserId);

            if (user != null)
            {
                repo.Delete(user);
            }

            await repo.SaveChangesAsync();
            return true;
        }

        public async Task<DisplayedManagerModel> GetCurrentManagerProfile(string userId)
        {
            var allUsers = await GetAllUsersAsync();
            var currentUser = allUsers.FirstOrDefault(u => u.UserId == userId);

            if (currentUser == null)
            {
                return null;
            }

            var manager = await repo.All<Manager>()
                                     .FirstOrDefaultAsync(e => e.UserId == userId);

            var salary = await repo.All<Salary>()
                                   .FirstOrDefaultAsync(s => s.UserId == userId);

            var department = await repo.All<Department>()
                                       .FirstOrDefaultAsync(d => d.ManagerId == manager.Id);

            return new DisplayedManagerModel
            {
                UserId = manager.UserId,
                Id = manager.Id,
                UserName = currentUser.UserName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Age = currentUser.Age,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                DepartmentName = department?.Name ?? "N/A",
                DepartmentId = department.Id,
                SalaryAmount = salary?.Amount ?? 0
            };
        }

        public async Task<IEnumerable<DisplayedManagerModel>> GetAllManagersAsync()
        {
            var managers = await repo.All<Manager>()
                .Include(m => m.User)
                .Include(m => m.Department)
                .ToListAsync();

            var salaries = await repo.All<Salary>().ToListAsync();

            return managers.Select(m => new DisplayedManagerModel
            {
                Id = m.Id,
                UserId = m.User.Id,
                FirstName = m.User.FirstName,
                LastName = m.User.LastName,
                UserName = m.User.UserName,
                Age = m.User.Age,
                Email = m.User.Email,
                PhoneNumber = m.User.PhoneNumber,
                DepartmentName = m.Department.Name,
                DepartmentId = m.Department.Id,
                SalaryAmount = salaries.FirstOrDefault(s => s.UserId == m.User.Id)?.Amount ?? 0
            }).ToList();
        }

        public async Task EditManagerAsync(DisplayedManagerModel model)
        {
            var user = await repo.GetByIdAsync<User>(model.UserId);
            var salary = await repo.All<Salary>().FirstOrDefaultAsync(s => s.UserId == model.UserId);

            if (user == null || salary == null)
            {
                throw new ArgumentException("Invalid manager");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Age = model.Age;

            salary.Amount = model.SalaryAmount;

            await repo.SaveChangesAsync();
        }


        public async Task<IEnumerable<DisplayedUserModel>> GetAllNotVerifiedUsersAsync()
        {
            var users = repo.All<User>();
            var result = users
                .Where(u => !u.IsVerified)
                .Select(u => new DisplayedUserModel
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsVerified = u.IsVerified,
                    Role = ""
                })
                .ToList();

            return result;
        }

        public async Task ApproveUserAsync(ApproveUserInputModel model)
        {
            var user = await repo.GetByIdAsync<User>(model.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.IsVerified = true;

            // Add to role
            var role = await repo.All<Role>().FirstOrDefaultAsync(r => r.Name == model.Role);
            if (role == null)
            {
                throw new ArgumentException("Invalid role name");
            }

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            await repo.AddAsync(userRole);

            // Add to department and create Employee/Manager
            var department = await repo.GetByIdAsync<Department>(model.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException("Invalid department");
            }

            if (model.Role == "Employee")
            {
                var employee = new Employee
                {
                    UserId = user.Id,
                    DepartmentId = department.Id,
                     Position = model.Position
                };
                await repo.AddAsync(employee);
            }

            // Add salary
            var salary = new Salary
            {
                UserId = user.Id,
                Amount = model.Salary
            };
            await repo.AddAsync(salary);

            await repo.SaveChangesAsync();
        }



    }
}
