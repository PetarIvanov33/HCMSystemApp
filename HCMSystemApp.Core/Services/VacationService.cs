using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Vacation;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Core.Services
{
    public class VacationService : IVacationService
    {

        private readonly IRepository repository;

        public VacationService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<VacationViewModel>> GetAllVacationsAsync()
        {
            return await repository
         .AllReadonly<Vacation>()
         .Include(v => v.User)
         .Select(v => new VacationViewModel
         {
             Id = v.Id,
             StartDate = v.StartDate.ToShortDateString(),
             EndDate = v.EndDate.ToShortDateString(),
             Reason = v.Reason,
             IsApproved = v.IsApproved,
             IsReviewed = v.IsReviewed,
             UserFullName = $"{v.User.FirstName} {v.User.LastName}",
             UserEmail = v.User.Email
         })
         .ToListAsync();
        }

        public async Task<IEnumerable<VacationViewModel>> GetCurrentUserVacationAsync(string userId)
        {
            return await repository
                .AllReadonly<Vacation>()
                .Where(v => v.UserId == userId)
                .Include(v => v.User)
                .Select(v => new VacationViewModel
                {
                    Id = v.Id,
                    StartDate = v.StartDate.ToShortDateString(),
                    EndDate = v.EndDate.ToShortDateString(),
                    Reason = v.Reason,
                    IsApproved = v.IsApproved,
                    IsReviewed = v.IsReviewed,
                    UserFullName = $"{v.User.FirstName} {v.User.LastName}",
                    UserEmail = v.User.Email
                })
                .ToListAsync();
        }

    }
}
