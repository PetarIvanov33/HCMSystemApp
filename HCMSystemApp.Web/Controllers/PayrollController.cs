using System.Security.Claims;
using HCMSystemApp.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCMSystemApp.Web.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPayrollService payrollService;

        public PayrollController(IPayrollService _payrollService)
        {
            payrollService = _payrollService;
        }

        [HttpGet]
        [Authorize(Roles = "Employee, Manager")]
        public async Task<IActionResult> MyPayrolls()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var payrolls = await payrollService.GetCurrentUserPayrollsAsync(userId);

            return View("MyPayrolls", payrolls);
        }
        
    }
}
