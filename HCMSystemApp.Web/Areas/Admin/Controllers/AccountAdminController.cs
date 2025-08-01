using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Services;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "HRAdmin")]
    public class AccountAdminController : Controller
    {
        private readonly IAccountService accountService;

        private readonly IRoleService roleService;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public AccountAdminController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAccountService _accountService,
            IRoleService _roleService
            )
        {
            userManager = _userManager;
            signInManager = _signInManager;
            accountService = _accountService;
            roleService = _roleService;
        }

        [Area("Admin")]
        public async Task<IActionResult> PendingUsers()
        {
            var users = await accountService.GetAllNotVerifiedUsersAsync();
            return View("PendingUsers", users);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
