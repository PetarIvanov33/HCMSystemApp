using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Payroll;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Core.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IRepository repo;

        public PayrollService(IRepository _repo)
        {
            repo = _repo;

        }
        public async Task<IEnumerable<PayrollViewModel>> GetAllPayrollsAsync()
        {
            return await repo.AllReadonly<Payroll>()
                .Select(p => new PayrollViewModel
                {
                    Period = p.Period,
                    IssuedOn = p.IssuedOn,
                    Bonus = p.Bonus,
                    TaxAmount = p.TaxAmount,
                    NetAmount = p.NetAmount,
                    BaseSalary = p.Salary.Amount,
                    UserId = p.UserId,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PayrollViewModel>> GetCurrentUserPayrollsAsync(string userId)
        {
            var allPayrolls = await GetAllPayrollsAsync();
            return allPayrolls.Where(p => p.UserId == userId); 
        }



    }
}
