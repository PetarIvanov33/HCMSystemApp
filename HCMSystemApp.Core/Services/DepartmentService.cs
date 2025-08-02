using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMSystemApp.Core.Contracts;
using HCMSystemApp.Core.Models.Department;
using HCMSystemApp.Infrastructure.Data.Common;
using HCMSystemApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HCMSystemApp.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository repo;

        public DepartmentService(IRepository _repo)
        {
            repo = _repo;

        }
        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartments()
        {
            return await repo.AllReadonly<Department>().
                Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                }).ToListAsync();

        }
    }
}
