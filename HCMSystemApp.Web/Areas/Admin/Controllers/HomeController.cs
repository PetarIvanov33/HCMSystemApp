using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for the HR Admin home dashboard.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "HRAdmin")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays the HR Admin home page.
        /// </summary>
        /// <returns>The home view for the Admin area.</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
