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
    /// <summary>
    /// Service for managing user roles, including assigning roles to users and retrieving role information.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="_repo">Repository instance for database access.</param>
        public RoleService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Assigns a specific role to a given user.
        /// </summary>
        /// <param name="user">The user entity to assign the role to.</param>
        /// <param name="roleName">The name of the role to assign.</param>
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

        /// <summary>
        /// Retrieves a role entity by its name.
        /// </summary>
        /// <param name="roleName">The name of the role to retrieve.</param>
        /// <returns>The <see cref="Role"/> entity if found, otherwise null.</returns>
        public async Task<Role> GetRole(string roleName)
        {
            var role = await repo.All<Role>().FirstOrDefaultAsync(x => x.Name == roleName);

            return role;
        }
    }
}
