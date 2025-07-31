using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Vacation;

namespace HCMSystemApp.Core.Contracts
{
    public interface IVacationService
    {
        Task<IEnumerable<VacationViewModel>> GetAllVacationsAsync();

        Task<IEnumerable<VacationViewModel>> GetCurrentUserVacationAsync(string userId);
    }

}
