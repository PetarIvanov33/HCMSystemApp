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
    /// <summary>
    /// Service for managing payroll operations including salary retrieval,
    /// payroll listing, and adding new payroll entries.
    /// </summary>
    public class PayrollService : IPayrollService
    {
        private readonly IRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayrollService"/> class.
        /// </summary>
        /// <param name="_repo">Repository instance for database access.</param>
        public PayrollService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Retrieves the salary information for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A <see cref="SalaryViewModel"/> containing the gross salary.</returns>
        public async Task<SalaryViewModel> GetUserSalary(string userId)
        {
            var salary = await repo.AllReadonly<Salary>()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            return new SalaryViewModel { GrossSalary = salary.Amount };
        }

        /// <summary>
        /// Retrieves all payroll entries.
        /// </summary>
        /// <returns>A collection of <see cref="PayrollViewModel"/>.</returns>
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

        /// <summary>
        /// Retrieves all payroll entries for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of <see cref="PayrollViewModel"/> belonging to the user.</returns>
        public async Task<IEnumerable<PayrollViewModel>> GetCurrentUserPayrollsAsync(string userId)
        {
            var allPayrolls = await GetAllPayrollsAsync();
            return allPayrolls.Where(p => p.UserId == userId);
        }

        /// <summary>
        /// Adds a new payroll entry for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="model">The payroll data to add.</param>
        /// <returns>True if the payroll was successfully added, otherwise false.</returns>
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
