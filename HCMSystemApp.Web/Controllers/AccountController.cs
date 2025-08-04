using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Users;
using HCMSystemApp.Infrastructure.Data.Entities;
using HCMSystemApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HCMSystemApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        private readonly IRoleService roleService;

        private readonly IDepartmentService departmentService;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public AccountController(
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

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Age = model.Age,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DisplayedProfileForEmployee()
        {
            try
            {
                var employee = await accountService.GetCurrentEmployeeProfile(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View("DisplayedProfileForEmployee", employee);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Manager, HRAdmin")]
        public async Task<IActionResult> EditEmployeeProfile(string id)
        {
            var employee = await accountService.GetCurrentEmployeeProfile(id);

            ViewBag.Departments = new SelectList(employee.Departments, "Id", "Name");

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, HRAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployeeProfile(DisplayedEmployeeModel model, string? managerIdOfEmployee)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    string key = state.Key;
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
                    }
                }
                return View(model);
            }

            try
            {
                await accountService.UpdateEmployeeAsync(model);
                TempData["Success"] = "Employee profile updated successfully!";
                return RedirectToAction("MyDepartment", "Department", new {Id = managerIdOfEmployee});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager, HRAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(string id, string? managerIdOfEmployee)
        {
            string managerId = null;

            if (User.IsInRole("Manager"))
            {
                managerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            else if (User.IsInRole("HRAdmin"))
            {
                managerId = managerIdOfEmployee;
            }

            bool success = await accountService.DeleteEmployeeAsync(id, managerId);

            if (!success)
                return Forbid();

            return RedirectToAction("MyDepartment", "Department", new { Id = managerIdOfEmployee });
        }



        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DisplayedProfileForManager()
        {
            try
            {
                return View(await accountService.GetCurrentManagerProfile(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }



    }
}
