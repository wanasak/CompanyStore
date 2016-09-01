using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Service
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetDepartments();
        IEnumerable<object> GetDepartmentEmployeeChart();
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IEntityBaseRepository<Department> _departmentRepository;

        public DepartmentService(IEntityBaseRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _departmentRepository.GetAll().AsEnumerable();
        }
        public IEnumerable<object> GetDepartmentEmployeeChart()
        {
            var result = _departmentRepository.GetAll()
                .Select(d => new
                {
                    label = d.Name,
                    value = d.Employees.Count
                });
            return result;
        }
    }
}
