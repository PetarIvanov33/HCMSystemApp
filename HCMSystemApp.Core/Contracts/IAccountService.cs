using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Users;
using HCMSystemApp.Infrastructure.Data.Entities;


namespace HCMSystemApp.Core.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<DisplayedUserModel>> GetAllUsersAsync();

        Task<DisplayedUserModel> GetCurrentUserProfile(string userId);

        Task<DisplayedEmployeeModel> GetCurrentEmployeeProfile(string userId);

        Task UpdateEmployeeAsync(DisplayedEmployeeModel model);

        Task<bool> DeleteEmployeeAsync(string userIdToDelete, string managerUserId);

        Task<DisplayedManagerModel> GetCurrentManagerProfile(string userId);

        Task<IEnumerable<DisplayedUserModel>> GetAllNotVerifiedUsersAsync();

        Task ApproveUserAsync(ApproveUserInputModel model);


    }
}
