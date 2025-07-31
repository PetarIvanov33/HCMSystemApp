using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models;


namespace HCMSystemApp.Core.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<DisplayedUserModel>> GetAllUsersAsync();
        Task<DisplayedUserModel> GetCurrentUserProfile(string userId);
    }
}
