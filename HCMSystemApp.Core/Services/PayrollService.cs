using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public async Task<SalaryViewModel> GetUserSalary(string userId)
        {
            var salary = await repo.AllReadonly<Salary>()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            return new SalaryViewModel { GrossSalary = salary.Amount };
        }

        public async Task<IEnumerable<PayrollViewModel>> GetAllPayrollsAsync()
        {
            return await repo.AllReadonly<Payroll>()
                .Select(p => new PayrollViewModel
                {
                    Id = p.Id,
                    Period = p.Period,
                    IssuedOn = p.IssuedOn,
                    Bonus = p.Bonus,
                    TaxAmount = p.TaxAmount,
                    NetAmount = p.NetAmount,
                    BaseSalary = p.GrossAmount,
                    UserId = p.UserId,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PayrollViewModel>> GetCurrentUserPayrollsAsync(string userId)
        {
            var allPayrolls = await GetAllPayrollsAsync();
            return allPayrolls.Where(p => p.UserId == userId); 
        }

        public async Task<bool> AddPayrollAsync(string userId, PayrollViewModel model)
        {
            var salary = await repo
                .All<Salary>()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (salary == null)
                return false;

            model.BaseSalary = salary.Amount;

            model.TaxAmount = (model.Bonus + model.BaseSalary) * 0.1m;
            model.NetAmount = (model.Bonus + model.BaseSalary) * 0.9m;

            if (model.IssuedOn == default)
            {
                model.IssuedOn = DateTime.UtcNow;
            }

            var payroll = new Payroll
            {
                Period = model.Period,
                IssuedOn = model.IssuedOn,
                Bonus = model.Bonus,
                TaxAmount = model.TaxAmount,
                NetAmount = model.NetAmount,
                GrossAmount = salary.Amount,
                UserId = userId,
            };

            await repo.AddAsync(payroll);
            await repo.SaveChangesAsync();

            return true;
        }




    }
}
