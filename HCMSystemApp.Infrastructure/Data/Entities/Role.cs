using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HCMSystemApp.Infrastructure.Data.Entities
{
    public class Role : IdentityRole
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

       
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
