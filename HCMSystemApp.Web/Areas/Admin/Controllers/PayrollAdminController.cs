using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Payroll;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "HRAdmin")]
    public class PayrollAdminController : Controller
    {
        private readonly IPayrollService payrollService;

        public PayrollAdminController(IPayrollService _payrollService)
        {
            payrollService = _payrollService;
        }

        [HttpGet]
        public async Task<IActionResult> AddManagerPayroll(string id)
        {
            var salary = await payrollService.GetUserSalary(id);

            if (salary == null)
                return NotFound();

            var model = new PayrollViewModel
            {
                UserId = id,
                BaseSalary = salary.GrossSalary,
                IssuedOn = DateTime.UtcNow
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddManagerPayroll(string userId, PayrollViewModel model)
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
            return RedirectToAction("AllManagers", "AccountAdmin");
        }
    }
}
