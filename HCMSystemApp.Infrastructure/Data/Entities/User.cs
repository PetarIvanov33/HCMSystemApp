using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HCMSystemApp.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string LastName { get; set; } = null!;

        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        public Salary? Salary { get; set; }

        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
        public ICollection<Vacation> Vacations { get; set; } = new List<Vacation>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
