using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Infrastructure.Data.Entities;

namespace HCMSystemApp.Core.Contracts
{
    public interface IRoleService
    {
        Task<Role> GetRole(string roleName);

        Task AddToRole(User user, string roleName);
    }
}
