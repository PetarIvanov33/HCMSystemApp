using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HCMSystemApp.Infrastructure.Data.Entities
{
     public class UserRole : IdentityUserRole<string>
    {
        
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
