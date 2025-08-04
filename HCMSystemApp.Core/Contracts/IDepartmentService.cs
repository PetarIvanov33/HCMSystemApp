using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Core.Models.Users;

namespace HCMSystemApp.Core.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartments();

        Task<DepartmentViewModel?> GetDepartmentByManagerUserIdAsync(string managerUserId);

        Task<IEnumerable<DisplayedEmployeeModel>> GetEmployeesByDepartmentIdAsync(int departmentId);

        Task<bool> UpdateDepartmentNameAsync(int departmentId, string newName);

        Task<bool> CreateDepartmentWithManagerAsync(AddManagerAndDepartmentModel model);

    }
}
