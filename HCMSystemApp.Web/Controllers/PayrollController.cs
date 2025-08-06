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
        private readonly IDepartmentService departmentService;

        public PayrollController(IPayrollService _payrollService, IDepartmentService _departmentService)
        {
            payrollService = _payrollService;
            departmentService = _departmentService;
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
        [Authorize(Roles = "Manager, HRAdmin")]
        public async Task<IActionResult> AddPayroll(string Id)
        {
            var salary = await payrollService.GetUserSalary(Id);

            var managerIdOfEmployee = await departmentService.GetEmployeeManager(Id);

            if (salary == null)
                return NotFound();

            var model = new PayrollViewModel
            {
                UserId = Id,
                BaseSalary = salary.GrossSalary,
                IssuedOn = DateTime.UtcNow,
                ManagerIdOfEmployee = managerIdOfEmployee,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, HRAdmin")]
        public async Task<IActionResult> AddPayroll(string userId, PayrollViewModel model, string? manager)
        {
            if (!ModelState.IsValid)
            {
                //For Debug
                //foreach (var modelState in ModelState.Values)
                //{
                //    foreach (var error in modelState.Errors)
                //    {
                //        Console.WriteLine(error.ErrorMessage);
                //    }
                //}
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
            return RedirectToAction("MyDepartment", "Department", new {Id = manager });
        }


    }
}
