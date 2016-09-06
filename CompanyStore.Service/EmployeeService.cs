using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Data.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Service
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees(string filter);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEntityBaseRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(
            IEntityBaseRepository<Employee> employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();
            return employees;
        }
        public Employee GetEmployee(int id)
        {
            var employee = _employeeRepository.GetSingle(id);
            return employee;
        }
        public IEnumerable<Employee> GetEmployees(string filter)
        {
            var employees = _employeeRepository.GetEmployeeByFilter(filter);
            return employees;
        }
        public void CreateEmployee(Employee employee)
        {
            employee.IsActive = true;
            employee.UniqueKey = Guid.NewGuid();
            _employeeRepository.Add(employee);
            _unitOfWork.Commit();
        }
        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Edit(employee);
            _unitOfWork.Commit();
        }
        public void DeleteEmployee(int id)
        {
            Employee employee = new Employee() { ID = id };
            _employeeRepository.Delete(employee);
            _unitOfWork.Commit();
        }
    }
}
