using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HCMSystemApp.Core.Models.Vacation
{
    public class VacationFormModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(300)]
        public string? Reason { get; set; }
    }
}
