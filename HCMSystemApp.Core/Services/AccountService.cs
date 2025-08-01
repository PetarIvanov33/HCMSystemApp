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

        //public async Task<DisplayedEmployeeModel> GetCurrentUserProfile(string userId)
        //{
        //    var users = await GetAllUsersAsync();
        //    return users.First(u => u.UserId == userId);
        //}


        public async Task<DisplayedEmployeeModel?> GetCurrentEmployeeProfile(string userId)
        {
            
            var allUsers = await GetAllUsersAsync();
            var currentUser = allUsers.FirstOrDefault(u => u.UserId == userId);

            if (currentUser == null)
            {
                return null;
            }

            var employee = await repo.All<Employee>()
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
                Position = employee?.Position ?? "N/A",
                SalaryAmount = salary?.Amount ?? 0
            };
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


    }
}
