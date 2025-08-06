using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Payroll;

namespace HCMSystemApp.Core.Contracts
{
    /// <summary>
    /// Contract for managing payroll operations including salary retrieval, payroll listing, and adding payrolls.
    /// </summary>
    public interface IPayrollService
    {
        /// <summary>
        /// Retrieves all payroll entries in the system.
        /// </summary>
        /// <returns>A collection of <see cref="PayrollViewModel"/>.</returns>
        Task<IEnumerable<PayrollViewModel>> GetAllPayrollsAsync();

        /// <summary>
        /// Retrieves all payroll entries for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose payrolls to retrieve.</param>
        /// <returns>A collection of <see cref="PayrollViewModel"/>.</returns>
        Task<IEnumerable<PayrollViewModel>> GetCurrentUserPayrollsAsync(string userId);

        /// <summary>
        /// Adds a new payroll entry for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="model">The payroll details.</param>
        /// <returns>True if the payroll was successfully added, otherwise false.</returns>
        Task<bool> AddPayrollAsync(string userId, PayrollViewModel model);

        /// <summary>
        /// Retrieves the salary information for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A <see cref="SalaryViewModel"/> containing the user's salary details.</returns>
        Task<SalaryViewModel> GetUserSalary(string userId);
    }
}
