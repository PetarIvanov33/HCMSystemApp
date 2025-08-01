using System.Diagnostics;
using HCMSystemApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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


        public IActionResult Start()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        public IActionResult HomeForEmployee()
        {
            return View();
        }


        [Authorize(Roles = "Manager")]
        public IActionResult HomeForManager()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

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
