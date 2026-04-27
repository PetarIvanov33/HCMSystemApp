using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HCMSystemApp.Core.Models.Vacation;

namespace HCMSystemApp.Web.Controllers
{
    /// <summary>
    /// Controller for managing vacation-related actions for employees and managers.
    /// </summary>
    public class VacationController : Controller
    {
        private readonly IVacationService vacationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VacationController"/> class.
        /// </summary>
        /// <param name="_vacationService">Service for handling vacation-related operations.</param>
        public VacationController(IVacationService _vacationService)
        {
            vacationService = _vacationService;
        }

        /// <summary>
        /// Displays the list of vacations for the currently logged-in employee or manager.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Employee, Manager")]
        public async Task<IActionResult> MyVacations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var model = await vacationService.GetCurrentUserVacationAsync(userId);
            return View("MyVacations", model);
        }

        [HttpGet]
        [Authorize(Roles = "Employee, Manager")]
        public IActionResult RequestVacation()
        {
            var model = new VacationFormModel
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(4)
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employee, Manager")]
        public async Task<IActionResult> RequestVacation(VacationFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            await vacationService.CreateVacationAsync(userId, model);

            return RedirectToAction(nameof(MyVacations));
        }

        [HttpGet]
        [Authorize(Roles = "Manager, HRAdmin")]
        public async Task<IActionResult> PendingVacations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var isHrAdmin = User.IsInRole("HRAdmin");

            var model = await vacationService.GetPendingVacationsForApprovalAsync(userId, isHrAdmin);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, HRAdmin")]
        public async Task<IActionResult> ApproveVacation(int id)
        {
            await vacationService.ApproveVacationAsync(id);

            return RedirectToAction(nameof(PendingVacations));
        }

        [HttpPost]
        [Authorize(Roles = "Manager, HRAdmin")]
        public async Task<IActionResult> RejectVacation(int id)
        {
            await vacationService.RejectVacationAsync(id);

            return RedirectToAction(nameof(PendingVacations));
        }
    }
}
