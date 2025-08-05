using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Core.Services;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "HRAdmin")]
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

        [HttpGet]
        public async Task<IActionResult> AllDepartments()
        {
            var departments = await departmentService.GetAllDepartments();
            return View(departments);
        }

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

        [HttpGet]
        public IActionResult CreateDepartmentWithManager()
        {
            return View(new AddManagerAndDepartmentModel());
        }

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
