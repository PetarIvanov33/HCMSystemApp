using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Vacation;

namespace HCMSystemApp.Core.Contracts
{
    /// <summary>
    /// Contract for managing vacation requests and retrieving vacation information.
    /// </summary>
    public interface IVacationService
    {
        /// <summary>
        /// Retrieves all vacation requests in the system.
        /// </summary>
        /// <returns>A collection of <see cref="VacationViewModel"/> representing all vacations.</returns>
        Task<IEnumerable<VacationViewModel>> GetAllVacationsAsync();

        /// <summary>
        /// Retrieves all vacation requests for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose vacations to retrieve.</param>
        /// <returns>A collection of <see cref="VacationViewModel"/> representing the user's vacations.</returns>
        Task<IEnumerable<VacationViewModel>> GetCurrentUserVacationAsync(string userId);
    }
}
