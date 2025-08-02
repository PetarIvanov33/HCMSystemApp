using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Users;
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
                Role = "" // може да го попълниш, ако имаш информация за ролята
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
                                     .FirstOrDefaultAsync(e => e.UserId == userId);

            var salary = await repo.All<Salary>()
                                   .FirstOrDefaultAsync(s => s.UserId == userId);

            return new DisplayedEmployeeModel
            {
                UserId = currentUser.UserId,
                UserName = currentUser.UserName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Age = currentUser.Age,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                Department = employee.Department.Name,
                Position = employee?.Position ?? "N/A",
                SalaryAmount = salary?.Amount ?? 0
            };
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
                return false; // Няма право да трие

            // Изтриваме заплата
            var salary = await repo.All<Salary>()
                .FirstOrDefaultAsync(s => s.UserId == userIdToDelete);
            if (salary != null) repo.Delete(salary);

            // Изтриваме пейроли
            var payrolls = await repo.All<Payroll>()
                .Where(p => p.UserId == userIdToDelete)
                .ToListAsync();
            repo.DeleteRange(payrolls);

            // Изтриваме ролята
            var userRoles = await repo.All<UserRole>()
                .Where(ur => ur.UserId == userIdToDelete)
                .ToListAsync();
            repo.DeleteRange(userRoles);

            // Изтриваме employee
            repo.Delete(employee);

            // Изтриваме самия потребител
            var user = await repo.GetByIdAsync<User>(userIdToDelete);
            repo.Delete(user);

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
                UserId = currentUser.UserId,
                Id = manager.Id,
                UserName = currentUser.UserName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Age = currentUser.Age,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                DepartmentName = department?.Name ?? "N/A",
                SalaryAmount = salary?.Amount ?? 0
            };
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
