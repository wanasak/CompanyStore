using CompanyStore.Data;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Test.Services
{
    [TestFixture]
    public class EmployeeTestService
    {
        #region Variables
        private List<Employee> _randomEmployees;
        private IEntityBaseRepository<Employee> _employeeRepository;
        private IUnitOfWork _unitOfwork;
        private IEmployeeService _employeeService;
        #endregion
        #region Setup
        [SetUp]
        public void Setup()
        {
            _randomEmployees = SetupEmployees();
            _employeeRepository = SetupEmployeeRepository();
            _unitOfwork = new Mock<IUnitOfWork>().Object;
            _employeeService = new EmployeeService(_employeeRepository, _unitOfwork);
        }
        public List<Employee> SetupEmployees()
        {
            int counter = new int();
            List<Employee> employees = MockDataInitializer.GenerateEmployees();
            foreach (Employee e in employees)
                e.ID = ++counter;
            return employees;
        }
        public IEntityBaseRepository<Employee> SetupEmployeeRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Employee>>();
            // Setup behavior
            repo.Setup(r => r.GetAll())
                .Returns(_randomEmployees.AsQueryable());
            repo.Setup(r => r.GetSingle(It.IsAny<int>()))
                .Returns(new Func<int, Employee>(
                    id => _randomEmployees.Find(e => e.ID == id)
                    ));
            repo.Setup(r => r.Add(It.IsAny<Employee>()))
                .Callback(new Action<Employee>(
                    newEmployee =>
                    {
                        dynamic maxID = _randomEmployees.Last().ID;
                        dynamic nextID = maxID + 1;
                        newEmployee.ID = nextID;
                        newEmployee.CreatedDate = DateTime.Now;
                        _randomEmployees.Add(newEmployee);
                    }));
            repo.Setup(r => r.Edit(It.IsAny<Employee>()))
                .Callback(new Action<Employee>(
                    updateEmployee =>
                    {
                        var employee = _randomEmployees.Find(e => e.ID == updateEmployee.ID);
                        employee = updateEmployee;
                    }));
            repo.Setup(r => r.Delete(It.IsAny<Employee>()))
                .Callback(new Action<Employee>(
                    deleteEmployee =>
                    {
                        var employee = _randomEmployees.Find(e => e.ID == deleteEmployee.ID);
                        if (employee != null)
                            _randomEmployees.Remove(employee);
                    }));
            return repo.Object;
        }
        #endregion
        #region Test
        [Test]
        public void ServiceShouldGetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();

            Assert.That(employees, Is.EqualTo(_randomEmployees));
        }
        [Test]
        public void ServiceShouldReturnRightEmployee()
        {
            var employee = _employeeService.GetEmployee(2);

            Assert.That(employee, Is.EqualTo(_randomEmployees.Find(e => e.ID == 2)));
        }
        [Test]
        public void ServiceShouldCreateEmployee()
        {
            var newEmployee = new Employee()
            {
                FirstName = "Wanasak",
                LastName = "Suraintaranggooon",
                Email = "abc@gmail.com",
                IsActive = true,
                UniqueKey = Guid.NewGuid()
            };
            int maxEmployeeID = _randomEmployees.Last().ID;
            _employeeService.CreateEmployee(newEmployee);

            Assert.That(newEmployee, Is.EqualTo(_randomEmployees.Last()));
            Assert.That(maxEmployeeID + 1, Is.EqualTo(_randomEmployees.Last().ID));
        }
        [Test]
        public void ServiceShouldUpdateEmployee()
        {
            var employee = _randomEmployees.First();
            employee.Email = "abc@gmail.com";
            _employeeService.UpdateEmployee(employee);

            Assert.That(employee.Email, Is.EqualTo("abc@gmail.com"));
            Assert.That(employee.ID, Is.EqualTo(1));
        }
        [Test]
        public void ServiceShouldDeleteEmployee()
        {
            var lastEmployeeID = _randomEmployees.Last().ID;
            _employeeService.DeleteEmployee(lastEmployeeID);

            Assert.That(lastEmployeeID, Is.GreaterThan(_randomEmployees.Last().ID));
        }
        #endregion
    }
}
