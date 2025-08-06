using System.Diagnostics;
using HCMSystemApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    /// <summary>
    /// Main application home controller handling navigation for different user roles and general pages.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for recording application events.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Redirects authenticated users to their respective home pages based on role, or shows the home page for unauthenticated users.
        /// </summary>
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == false)
            {
                return RedirectToAction("Start");
            }
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("HomeForEmployee");
            }
            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("HomeForManager");
            }
            if (User.IsInRole("HRAdmin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View();
        }

        /// <summary>
        /// Displays the start page for unauthenticated users.
        /// </summary>
        public IActionResult Start()
        {
            return View();
        }

        /// <summary>
        /// Displays the home page for employees.
        /// </summary>
        [Authorize(Roles = "Employee")]
        public IActionResult HomeForEmployee()
        {
            return View();
        }

        /// <summary>
        /// Displays the home page for managers.
        /// </summary>
        [Authorize(Roles = "Manager")]
        public IActionResult HomeForManager()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the access denied page for unauthorized requests.
        /// </summary>
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Displays a general or status-code-specific error page.
        /// </summary>
        /// <param name="statusCode">Optional HTTP status code to display a custom error page for.</param>
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }

            return View();
        }
    }
}
