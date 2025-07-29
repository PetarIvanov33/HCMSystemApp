using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Infrastructure.Data.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public Manager Manager { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
