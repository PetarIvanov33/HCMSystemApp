using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Payroll;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    /// <summary>
    /// Controller for managing payroll operations for employees, managers, and HR admins.
    /// </summary>
    public class PayrollController : Controller
    {
        private readonly IPayrollService payrollService;
        private readonly IDepartmentService departmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayrollController"/> class.
        /// </summary>
        /// <param name="_payrollService">Service for handling payroll operations.</param>
        /// <param name="_departmentService">Service for handling department-related operations.</param>
        public PayrollController(IPayrollService _payrollService, IDepartmentService _departmentService)
        {
            payrollService = _payrollService;
            departmentService = _departmentService;
        }

        /// <summary>
        /// Displays the list of payrolls for the currently logged-in employee or manager.
        /// </summary>
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

        /// <summary>
        /// Displays the form for adding a payroll entry for an employee or manager.
        /// </summary>
        /// <param name="Id">The ID of the user for whom the payroll is being added.</param>
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

        /// <summary>
        /// Processes the creation of a payroll entry for an employee or manager.
        /// </summary>
        /// <param name="userId">The ID of the user receiving the payroll.</param>
        /// <param name="model">The payroll details.</param>
        /// <param name="manager">Optional ID of the manager who manages the employee.</param>
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
            return RedirectToAction("MyDepartment", "Department", new { Id = manager });
        }
    }
}
