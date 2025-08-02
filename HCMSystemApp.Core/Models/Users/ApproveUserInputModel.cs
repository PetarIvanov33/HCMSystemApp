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
        [MaxLength(50)]
        public string Position { get; set; } = null!;

        [Required]
        [Range(1033.00, double.MaxValue, ErrorMessage = "Salary must be greater than 1033.00")]
        public decimal Salary { get; set; }
    }
}
