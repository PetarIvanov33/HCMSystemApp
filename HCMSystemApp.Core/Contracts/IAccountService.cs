using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Users;
using HCMSystemApp.Infrastructure.Data.Entities;

namespace HCMSystemApp.Core.Contracts
{
    /// <summary>
    /// Contract for account management operations, including user, employee, and manager handling.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves all users except HR Admins.
        /// </summary>
        /// <returns>A collection of <see cref="DisplayedUserModel"/>.</returns>
        Task<IEnumerable<DisplayedUserModel>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves the profile of a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>A <see cref="DisplayedUserModel"/> representing the user.</returns>
        Task<DisplayedUserModel> GetCurrentUserProfile(string userId);

        /// <summary>
        /// Retrieves the employee profile for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the employee's user account.</param>
        /// <returns>A <see cref="DisplayedEmployeeModel"/> representing the employee.</returns>
        Task<DisplayedEmployeeModel> GetCurrentEmployeeProfile(string userId);

        /// <summary>
        /// Retrieves all employees who are not assigned to a department.
        /// </summary>
        /// <returns>A collection of <see cref="DisplayedEmployeeModel"/>.</returns>
        Task<IEnumerable<DisplayedEmployeeModel>> GetEmployeesWithoutDepartmentAsync();

        /// <summary>
        /// Assigns an employee to a specific department.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>True if the assignment was successful, otherwise false.</returns>
        Task<bool> AssignDepartmentToEmployeeAsync(string employeeId, int departmentId);

        /// <summary>
        /// Updates an employee's profile information.
        /// </summary>
        /// <param name="model">The updated employee model.</param>
        Task UpdateEmployeeAsync(DisplayedEmployeeModel model);

        /// <summary>
        /// Deletes an employee if they belong to the manager's department.
        /// </summary>
        /// <param name="userIdToDelete">The ID of the employee to delete.</param>
        /// <param name="managerUserId">The ID of the manager performing the deletion.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        Task<bool> DeleteEmployeeAsync(string userIdToDelete, string managerUserId);

        /// <summary>
        /// Deletes an employee who does not belong to any department.
        /// </summary>
        /// <param name="employeeId">The ID of the employee to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        Task<bool> DeleteEmployeeWithoutDepartmentAsync(string employeeId);

        /// <summary>
        /// Retrieves the manager profile for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the manager's user account.</param>
        /// <returns>A <see cref="DisplayedManagerModel"/> representing the manager.</returns>
        Task<DisplayedManagerModel> GetCurrentManagerProfile(string userId);

        /// <summary>
        /// Retrieves all managers in the system.
        /// </summary>
        /// <returns>A collection of <see cref="DisplayedManagerModel"/>.</returns>
        Task<IEnumerable<DisplayedManagerModel>> GetAllManagersAsync();

        /// <summary>
        /// Edits a manager's profile information.
        /// </summary>
        /// <param name="model">The updated manager model.</param>
        Task EditManagerAsync(DisplayedManagerModel model);

        /// <summary>
        /// Retrieves all users who have not yet been verified.
        /// </summary>
        /// <returns>A collection of <see cref="DisplayedUserModel"/>.</returns>
        Task<IEnumerable<DisplayedUserModel>> GetAllNotVerifiedUsersAsync();

        /// <summary>
        /// Approves a new user and assigns them a role, department, and salary.
        /// </summary>
        /// <param name="model">The model containing approval details.</param>
        Task ApproveUserAsync(ApproveUserInputModel model);
    }
}
