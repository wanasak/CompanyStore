using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Extension
{
    public static class DepartmentExtension
    {
        public static IEnumerable<object> GetDepartmentEmployeeChart(this IEntityBaseRepository<Department> departmentRepository)
        {
            var result = departmentRepository.GetAll()
                .Select(d => new
                {
                    label = d.Name,
                    value = d.Employees.Count
                });
            return result;
        }
    }
}
