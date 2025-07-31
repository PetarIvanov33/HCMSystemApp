using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSystemApp.Core.Models.Vacation
{
    public class VacationViewModel
    {
        public int Id { get; set; }

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string? Reason { get; set; }

        public bool IsApproved { get; set; }

        public bool IsReviewed { get; set; }

        public string UserFullName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;
    }
}
