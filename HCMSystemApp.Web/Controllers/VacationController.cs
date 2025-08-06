using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
