using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Payroll;

namespace HCMSystemApp.Core.Contracts
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollViewModel>> GetAllPayrollsAsync();
        Task<IEnumerable<PayrollViewModel>> GetCurrentUserPayrollsAsync(string userId);

        Task<bool> AddPayrollAsync(string userId, PayrollViewModel model);

        Task<SalaryViewModel> GetUserSalary(string userId);
    }
        
}
