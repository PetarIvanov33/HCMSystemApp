using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Core.Models.Users;
using HCMSystemApp.Core.Services;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for HR Admin operations related to accounts, managers, and department assignments.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "HRAdmin")]
    public class AccountAdminController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;
        private readonly IDepartmentService departmentService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountAdminController"/> class.
        /// </summary>
        public AccountAdminController(
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
        /// Displays a list of all pending (not verified) users.
        /// </summary>
        [Area("Admin")]
        public async Task<IActionResult> PendingUsers()
        {
            var users = await accountService.GetAllNotVerifiedUsersAsync();
            ViewBag.Departments = await departmentService.GetAllDepartments();
            return View("PendingUsers", users);
        }

        /// <summary>
        /// Displays the approval form for a specific user.
        /// </summary>
        [HttpGet]
        [Area("Admin")]
        public async Task<IActionResult> ApproveUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID is missing.");
            }

            var user = await accountService.GetCurrentUserProfile(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.IsVerified)
            {
                return RedirectToAction("PendingUsers");
            }

            var departments = await departmentService.GetAllDepartments();

            var model = new ApproveUserInputModel
            {
                UserId = id
            };

            ViewBag.UserDetails = user;
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(model);
        }

        /// <summary>
        /// Processes the approval of a user.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        public async Task<IActionResult> ApproveUser(ApproveUserInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewBag.Departments = new SelectList(await departmentService.GetAllDepartments(), "Id", "Name");
                ViewBag.UserDetails = await accountService.GetCurrentUserProfile(model.UserId);
                return View(model);
            }

            try
            {
                await accountService.ApproveUserAsync(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while approving the user..");
                ViewBag.Departments = new SelectList(await departmentService.GetAllDepartments(), "Id", "Name");
                ViewBag.UserDetails = await accountService.GetCurrentUserProfile(model.UserId);
                return View(model);
            }

            return RedirectToAction("PendingUsers");
        }

        /// <summary>
        /// Displays a list of all managers.
        /// </summary>
        [HttpGet]
        [Area("Admin")]
        public async Task<IActionResult> AllManagers()
        {
            var model = await accountService.GetAllManagersAsync();
            return View("AllManagers", model);
        }

        /// <summary>
        /// Displays the form for editing a manager's profile.
        /// </summary>
        [HttpGet]
        [Area("Admin")]
        public async Task<IActionResult> EditManagerProfile(string id)
        {
            var model = await accountService.GetCurrentManagerProfile(id);
            return View("EditManagerProfile", model);
        }

        /// <summary>
        /// Processes the editing of a manager's profile.
        /// </summary>
        [HttpPost]
        [Area("Admin")]
        public async Task<IActionResult> EditManagerProfile(DisplayedManagerModel model)
        {
            var mod = model;
            if (!ModelState.IsValid)
            {
                return View(mod);
            }

            await accountService.EditManagerAsync(mod);
            return RedirectToAction("AllManagers");
        }

        /// <summary>
        /// Displays employees who are not assigned to any department.
        /// </summary>
        [HttpGet]
        [Area("Admin")]
        public async Task<IActionResult> EmployeesWithoutDepartment()
        {
            var model = await accountService.GetEmployeesWithoutDepartmentAsync();
            var select = new SelectList(await departmentService.GetAllDepartmentsForSelect(), "Id", "Name");
            ViewBag.Departments = select;
            return View(model);
        }

        /// <summary>
        /// Assigns a department to an employee without one.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDepartment(string employeeId, int departmentId)
        {
            if (await accountService.AssignDepartmentToEmployeeAsync(employeeId, departmentId))
            {
                TempData["Success"] = "Department assigned successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to assign department.";
            }

            return RedirectToAction(nameof(EmployeesWithoutDepartment));
        }

        /// <summary>
        /// Deletes an employee who is not assigned to any department.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployeeWithoutDepartment(string id)
        {
            if (await accountService.DeleteEmployeeWithoutDepartmentAsync(id))
            {
                TempData["Success"] = "Employee deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete employee.";
            }

            return RedirectToAction(nameof(EmployeesWithoutDepartment));
        }

        /// <summary>
        /// Default action for the admin account area.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}
