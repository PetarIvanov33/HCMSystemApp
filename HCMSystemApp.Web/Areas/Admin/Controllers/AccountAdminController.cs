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
    [Area("Admin")]
    [Authorize(Roles = "HRAdmin")]
    public class AccountAdminController : Controller
    {
        private readonly IAccountService accountService;

        private readonly IRoleService roleService;

        private readonly IDepartmentService departmentService;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;


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

        [Area("Admin")]
        public async Task<IActionResult> PendingUsers()
        {
            var users = await accountService.GetAllNotVerifiedUsersAsync();

            ViewBag.Departments = await departmentService.GetAllDepartments();
            return View("PendingUsers", users);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        public async Task<IActionResult> ApproveUser(ApproveUserInputModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"Key: {state.Key}, IsValid: {state.Value?.Errors.Count == 0}, Errors: {string.Join(", ", state.Value?.Errors.Select(e => e.ErrorMessage))}");
                }
                // DEBUG: logни грешките за да ги видиш
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("Validation errors: " + string.Join(", ", errors));

                ViewBag.Departments = new SelectList(await departmentService.GetAllDepartments(), "Id", "Name");
                ViewBag.UserDetails = await accountService.GetCurrentUserProfile(model.UserId);
                return View(model);
            }

            try
            {
                await accountService.ApproveUserAsync(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message); // за дебъг

                ModelState.AddModelError(string.Empty, "Възникна грешка при одобряване на потребителя.");
                ViewBag.Departments = new SelectList(await departmentService.GetAllDepartments(), "Id", "Name");
                ViewBag.UserDetails = await accountService.GetCurrentUserProfile(model.UserId);
                return View(model);
            }

            return RedirectToAction("PendingUsers");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
