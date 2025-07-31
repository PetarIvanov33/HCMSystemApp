using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
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
                    //ProfileImageUrl = user.ProfileImageURL,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = role.Name
                }).ToListAsync();

            return users;
        }

        public async Task<DisplayedUserModel> GetCurrentUserProfile(string userId)
        {
            var users = await GetAllUsersAsync();
            return users.First(u => u.UserId == userId);
        }
    }
}
