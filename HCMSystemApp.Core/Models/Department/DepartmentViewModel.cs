using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCMSystemApp.Core.Models.Department
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "The department name must be between 2 and 50 characters.", MinimumLength = 2)]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = null!;

        [BindNever]
        public int ManagerId { get; set; }

        //[BindNever]
        public string UserIdOfManager { get; set; } = null!;


        [Display(Name = "Manager Name")]

        public string ManagerName = null!;

        public int EmployeeCount { get; set; }
    }
}
