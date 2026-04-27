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
    /// <summary>
    /// Service for managing vacation requests and retrieving vacation information.
    /// </summary>
    public class VacationService : IVacationService
    {
        private readonly IRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VacationService"/> class.
        /// </summary>
        /// <param name="_repository">Repository instance for database access.</param>
        public VacationService(IRepository _repository)
        {
            repository = _repository;
        }

        /// <summary>
        /// Retrieves all vacation requests from the system.
        /// </summary>
        /// <returns>A collection of <see cref="VacationViewModel"/> representing all vacations.</returns>
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

        /// <summary>
        /// Retrieves all vacation requests for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose vacations to retrieve.</param>
        /// <returns>A collection of <see cref="VacationViewModel"/> representing the user's vacations.</returns>
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

        public async Task CreateVacationAsync(string userId, VacationFormModel model)
        {
            if (model.StartDate > model.EndDate)
            {
                throw new ArgumentException("Start date cannot be after end date.");
            }

            var vacation = new Vacation
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Reason = model.Reason,
                IsApproved = false,
                IsReviewed = false,
                UserId = userId
            };

            await repository.AddAsync(vacation);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<VacationViewModel>> GetPendingVacationsForApprovalAsync(string userId, bool isHrAdmin)
        {
            var query = repository
                .AllReadonly<Vacation>()
                .Where(v => v.IsReviewed == false)
                .Include(v => v.User)
                .AsQueryable();

            if (!isHrAdmin)
            {
                var manager = await repository
                    .AllReadonly<Manager>()
                    .Include(m => m.Department)
                    .FirstOrDefaultAsync(m => m.UserId == userId);

                if (manager == null || manager.Department == null)
                {
                    return new List<VacationViewModel>();
                }

                var departmentId = manager.Department.Id;

                var employeeUserIds = await repository
                    .AllReadonly<Employee>()
                    .Where(e => e.DepartmentId == departmentId)
                    .Select(e => e.UserId)
                    .ToListAsync();

                query = query.Where(v => employeeUserIds.Contains(v.UserId));
            }
            else
            {
                var managerUserIds = await repository
                    .AllReadonly<Manager>()
                    .Select(m => m.UserId)
                    .ToListAsync();

                query = query.Where(v => managerUserIds.Contains(v.UserId));
            }

            return await query
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

        public async Task ApproveVacationAsync(int vacationId)
        {
            var vacation = await repository.GetByIdAsync<Vacation>(vacationId);

            if (vacation == null)
            {
                throw new ArgumentException("Vacation not found.");
            }

            vacation.IsApproved = true;
            vacation.IsReviewed = true;

            await repository.SaveChangesAsync();
        }

        public async Task RejectVacationAsync(int vacationId)
        {
            var vacation = await repository.GetByIdAsync<Vacation>(vacationId);

            if (vacation == null)
            {
                throw new ArgumentException("Vacation not found.");
            }

            vacation.IsApproved = false;
            vacation.IsReviewed = true;

            await repository.SaveChangesAsync();
        }
    }
}
