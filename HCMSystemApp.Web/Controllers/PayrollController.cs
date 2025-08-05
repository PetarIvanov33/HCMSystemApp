using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Payroll;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPayrollService payrollService;

        public PayrollController(IPayrollService _payrollService)
        {
            payrollService = _payrollService;
        }

        [HttpGet]
        [Authorize(Roles = "Employee, Manager")]
        public async Task<IActionResult> MyPayrolls()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var payrolls = await payrollService.GetCurrentUserPayrollsAsync(userId);

            return View("MyPayrolls", payrolls);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddPayroll(string Id)
        {
            var salary = await payrollService.GetUserSalary(Id);

            if (salary == null)
                return NotFound();

            var model = new PayrollViewModel
            {
                UserId = Id,
                BaseSalary = salary.GrossSalary,
                IssuedOn = DateTime.UtcNow 
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddPayroll(string userId, PayrollViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                var salary = await payrollService.GetUserSalary(userId);

                if (salary != null)
                {
                    model.BaseSalary = salary.GrossSalary;
                }

                return View(model);
            }

            var result = await payrollService.AddPayrollAsync(userId, model);
            if (!result)
            {
                TempData["Error"] = "Failed to add payroll.";
                return View(model);
            }

            TempData["Success"] = "Payroll added successfully.";
            return RedirectToAction("MyDepartment", "Department");
        }


    }
}
