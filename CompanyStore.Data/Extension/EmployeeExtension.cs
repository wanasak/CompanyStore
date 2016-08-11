using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Extension
{
    public static class EmployeeExtension
    {
        public static IEnumerable<Employee> GetEmployeeByFilter(this IEntityBaseRepository<Employee> employeeRepository, string filter = "")
        {
            return employeeRepository.GetAll().Where(e =>
                e.FirstName.ToLower().Contains(filter.ToLower().Trim()) ||
                e.LastName.ToLower().Contains(filter.ToLower().Trim()));
        }
    }
}
