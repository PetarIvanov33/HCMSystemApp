using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Core.Services;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for HR Admin operations related to departments and their managers.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "HRAdmin")]
    public class DepartmentController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;
        private readonly IDepartmentService departmentService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentController"/> class.
        /// </summary>
        public DepartmentController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAccountService _accountService,
            IRoleService _roleService,
            IDepartmentService _departmentService
            )
        {
            userManager = _userManager;
            signInManager = _signInManager;
            accountService = _accountService;
            roleService = _roleService;
            departmentService = _departmentService;
        }

        /// <summary>
        /// Displays a list of all departments.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> AllDepartments()
        {
            var departments = await departmentService.GetAllDepartments();
            return View(departments);
        }

        /// <summary>
        /// Displays the edit form for a department's details.
        /// </summary>
        /// <param name="Id">The user ID of the manager associated with the department.</param>
        [HttpGet]
        public async Task<IActionResult> EditDepartment(string Id)
        {
            var department = await departmentService.GetDepartmentByManagerUserIdAsync(Id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        /// <summary>
        /// Processes changes to a department's details.
        /// </summary>
        /// <param name="model">The updated department data.</param>
        [HttpPost]
        public async Task<IActionResult> EditDepartment(DepartmentViewModel model)
        {
            foreach (var state in ModelState)
            {
                var key = state.Key;
                var errors = state.Value.Errors;

                foreach (var error in errors)
                {
                    Console.WriteLine($"[ModelState] {key}: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await departmentService.UpdateDepartmentNameAsync(model.Id, model.Name);

            if (!success)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Department name updated successfully.";
            return RedirectToAction("AllDepartments");
        }

        /// <summary>
        /// Displays the form for creating a new department and assigning a manager to it.
        /// </summary>
        [HttpGet]
        public IActionResult CreateDepartmentWithManager()
        {
            return View(new AddManagerAndDepartmentModel());
        }

        /// <summary>
        /// Creates a new department and assigns a manager to it.
        /// </summary>
        /// <param name="model">The manager and department details.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartmentWithManager(AddManagerAndDepartmentModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await departmentService.CreateDepartmentWithManagerAsync(model);

            if (!success)
            {
                ModelState.AddModelError("", "Error creating user or assigning role.");
                return View(model);
            }

            return RedirectToAction("AllDepartments");
        }

        /// <summary>
        /// Deletes a department and its manager.
        /// </summary>
        /// <param name="id">The user ID of the manager associated with the department.</param>
        [HttpPost]
        public async Task<IActionResult> DeleteManagerAndDepartment(string id)
        {
            var result = await departmentService.DeleteManagerAndDepartmentAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("AllManagers", "AccountAdmin");
        }
    }
}
