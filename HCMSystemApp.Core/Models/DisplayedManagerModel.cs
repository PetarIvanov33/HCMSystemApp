using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Core.Models
{
    public class DisplayedManagerModel
    {
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public decimal SalaryAmount { get; set; }
    }
}
