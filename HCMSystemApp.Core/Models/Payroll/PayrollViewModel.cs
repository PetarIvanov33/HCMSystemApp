using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Core.Models.Payroll
{
    public class PayrollViewModel
    {
        public string Period { get; set; } = null!;
        public DateTime IssuedOn { get; set; }
        public decimal Bonus { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BaseSalary { get; set; }
        public string UserId { get; set; }
    }
}
