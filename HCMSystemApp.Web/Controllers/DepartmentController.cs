using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Services;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    /// <summary>
    /// Controller for managing department-related operations for managers and HR admins.
    /// </summary>
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
        /// Displays all employees in the current manager's or specified department.
        /// </summary>
        /// <param name="Id">
        /// For HRAdmin: the manager's user ID to view their department.  
        /// For Manager: ignored, department is retrieved from the logged-in manager's account.
        /// </param>
        [Authorize(Roles = "Manager, HRAdmin")]
        public async Task<IActionResult> MyDepartment(string Id)
        {
            string userId = null;

            if (User.IsInRole("Manager"))
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            else if (User.IsInRole("HRAdmin"))
            {
                userId = Id;
            }

            var department = await departmentService.GetDepartmentByManagerUserIdAsync(userId);

            if (department == null)
            {
                return NotFound("Department not found.");
            }

            var employees = await departmentService.GetEmployeesByDepartmentIdAsync(department.Id);

            return View("EmployeesOfDepartment", employees);
        }
    }
}
