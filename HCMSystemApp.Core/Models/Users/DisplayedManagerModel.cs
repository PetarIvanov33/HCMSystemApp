using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCMSystemApp.Core.Models.Users
{
    public class DisplayedManagerModel
    {
        public string UserId { get; set; } = null!;

        [BindNever]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Range(18, 70)]
        public int Age { get; set; }
       
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        
        [Display(Name = "Department")]
        public string? DepartmentName { get; set; } = null!;

        public int DepartmentId { get; set; }

        [Range(1033, 50000, ErrorMessage = "Salary must be at least 1033.")]
        [Display(Name = "Salary")]
        public decimal SalaryAmount { get; set; }
    }
}
