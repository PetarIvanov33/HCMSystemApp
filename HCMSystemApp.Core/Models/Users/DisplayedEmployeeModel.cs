using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Department;

namespace HCMSystemApp.Core.Models.Users
{
    public class DisplayedEmployeeModel
    {
        public string UserId { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Range(18, 65)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(100)]
        public string Position { get; set; } = null!;

        public string? Department { get; set; }

        public string? ManagerIdOfEmployee { get; set; }

        [Range(1033, double.MaxValue, ErrorMessage = "Salary must be at least 1033.")]
        public decimal SalaryAmount { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; } // for change employee department

        public IEnumerable<DepartmentDTO>? Departments { get; set; }
    }
}
