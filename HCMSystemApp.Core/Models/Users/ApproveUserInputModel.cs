using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Core.Models.Users
{
    public class ApproveUserInputModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
