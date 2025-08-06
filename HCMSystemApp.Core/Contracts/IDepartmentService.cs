using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Core.Models.Users;

namespace HCMSystemApp.Core.Contracts
{
    /// <summary>
    /// Contract for managing departments, their managers, and related employee operations.
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// Retrieves all departments with their managers and employee counts.
        /// </summary>
        /// <returns>A collection of <see cref="DepartmentViewModel"/>.</returns>
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartments();

        /// <summary>
        /// Retrieves all departments for selection lists.
        /// </summary>
        /// <returns>A collection of <see cref="DepartmentDTO"/>.</returns>
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsForSelect();

        /// <summary>
        /// Retrieves a department by the manager's user ID.
        /// </summary>
        /// <param name="managerUserId">The user ID of the manager.</param>
        /// <returns>A <see cref="DepartmentViewModel"/> or null if not found.</returns>
        Task<DepartmentViewModel?> GetDepartmentByManagerUserIdAsync(string managerUserId);

        /// <summary>
        /// Retrieves all employees assigned to a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>A collection of <see cref="DisplayedEmployeeModel"/>.</returns>
        Task<IEnumerable<DisplayedEmployeeModel>> GetEmployeesByDepartmentIdAsync(int departmentId);

        /// <summary>
        /// Updates the name of a department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <param name="newName">The new name for the department.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        Task<bool> UpdateDepartmentNameAsync(int departmentId, string newName);

        /// <summary>
        /// Creates a new department and assigns a manager to it.
        /// </summary>
        /// <param name="model">The model containing manager and department details.</param>
        /// <returns>True if creation was successful, otherwise false.</returns>
        Task<bool> CreateDepartmentWithManagerAsync(AddManagerAndDepartmentModel model);

        /// <summary>
        /// Deletes a department and its manager, unassigning employees and removing related data.
        /// </summary>
        /// <param name="managerId">The user ID of the manager.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        Task<bool> DeleteManagerAndDepartmentAsync(string managerId);

        /// <summary>
        /// Retrieves the user ID of the manager for a given employee.
        /// </summary>
        /// <param name="employeeUserId">The user ID of the employee.</param>
        /// <returns>The manager's user ID, or null if not found.</returns>
        Task<string?> GetEmployeeManager(string employeeUserId);
    }
}
