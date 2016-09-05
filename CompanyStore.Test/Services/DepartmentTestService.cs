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
    public class DepartmentTestService
    {
        #region Variables
        IDepartmentService _departmentService;
        IEntityBaseRepository<Department> _departmentRepository;
        IUnitOfWork _unitOfWork;
        List<Department> _randomDepartments;
        #endregion

        #region Setup
        [SetUp]
        public void Setup()
        {
            _randomDepartments = SetupDepartments();
            _departmentRepository = SetupDepartmentRepository();
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _departmentService = new DepartmentService(_departmentRepository);
        }
        public List<Department> SetupDepartments()
        {
            int counter = new int();
            List<Department> departments = MockDataInitializer.GenerateDepartments();

            foreach (Department d in departments)
                d.ID = ++counter;

            return departments;
        }
        public IEntityBaseRepository<Department> SetupDepartmentRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Department>>();
            // Setup behavior
            repo.Setup(r => r.GetAll())
                .Returns(_randomDepartments.AsQueryable());
            return repo.Object;
        }
        #endregion

        #region Tests
        [Test]
        public void ServiceShouldReturnAllDepartments()
        {
            var departments = _departmentService.GetDepartments();

            Assert.That(departments, Is.EqualTo(_randomDepartments));
        }
        #endregion
    }
}
