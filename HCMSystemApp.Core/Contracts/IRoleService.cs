using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Infrastructure.Data.Entities;

namespace HCMSystemApp.Core.Contracts
{
    /// <summary>
    /// Contract for managing user roles, including retrieving roles and assigning them to users.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Retrieves a role entity by its name.
        /// </summary>
        /// <param name="roleName">The name of the role to retrieve.</param>
        /// <returns>The <see cref="Role"/> entity if found, otherwise null.</returns>
        Task<Role> GetRole(string roleName);

        /// <summary>
        /// Assigns a specific role to a user.
        /// </summary>
        /// <param name="user">The user entity to assign the role to.</param>
        /// <param name="roleName">The name of the role to assign.</param>
        Task AddToRole(User user, string roleName);
    }
}
