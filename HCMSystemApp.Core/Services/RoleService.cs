using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository repo;

        public RoleService(IRepository _repo)
        {
            repo = _repo;

        }


        public async Task AddToRole(User user, string roleName)
        {
            var userRole = new UserRole()
            {
                User = user,
                Role = await GetRole(roleName)
            };

            await repo.AddAsync<UserRole>(userRole);
            await repo.SaveChangesAsync();
        }


        public async Task<Role> GetRole(string roleName)
        {
            var role = await repo.All<Role>().FirstOrDefaultAsync(x => x.Name == roleName);

            return role;
        }


    }
}
