using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Infrastructure.Data.Entities
{
    public class Vacation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(300)]
        public string? Reason { get; set; }

        [Required]
        public bool IsApproved { get; set; } = false;

        [Required]
        public bool IsReviewed { get; set; } = false;


        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}
