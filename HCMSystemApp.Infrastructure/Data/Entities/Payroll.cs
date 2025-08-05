using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Infrastructure.Data.Entities
{
    public class Payroll
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Period { get; set; } = null!; 

        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonus { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal  GrossAmount { get; set; }

       
        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

       
        //[Required]
        //public int SalaryId { get; set; }

        //[ForeignKey(nameof(SalaryId))]
        //public Salary Salary { get; set; } = null!;
    }
}
