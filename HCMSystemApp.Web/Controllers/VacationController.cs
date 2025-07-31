using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    public class VacationController : Controller
    {
        private readonly IVacationService vacationService;

        public VacationController(IVacationService _vacationService)
        {
            vacationService = _vacationService;
        }

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
