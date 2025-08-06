using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Core.Models.Payroll
{
    public class PayrollViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Period { get; set; } = null!;
        public DateTime IssuedOn { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Bonus { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BaseSalary { get; set; }

        public string? ManagerIdOfEmployee { get; set; }
        public string UserId { get; set; }
    }
}
