using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Core.Models
{
    public class DisplayedUserModel
    {
        public string UserId { get; set; } = null!;
        //public string? ProfileImageUrl { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = null!;
    }
}
