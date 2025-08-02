using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Services;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IAccountService accountService;

        private readonly IRoleService roleService;

        private readonly IDepartmentService departmentService;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;


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

        [Authorize(Roles = "Manager")]
public async Task<IActionResult> MyDepartment()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
