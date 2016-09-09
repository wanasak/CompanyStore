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
    public class RentalTestService
    {
        #region Variables
        private List<Rental> _randomRentals;
        private List<Employee> _randomEmployee;
        private List<Device> _randomDevices;
        private List<Stock> _randomStocks;
        private IRentalService _rentalService;
        private IUnitOfWork _unitOfWork;
        private IEntityBaseRepository<Rental> _rentalRepository;
        private IEntityBaseRepository<Device> _deviceRepository;
        private IEntityBaseRepository<Employee> _employeeRepository;
        private IEntityBaseRepository<Stock> _stockRepository;
        #endregion
        #region Setup
        [SetUp]
        public void Setup()
        {
            _randomStocks = SetupStocks();
            _randomDevices = SetupDevices();
            _randomEmployee = SetupEmployees();
            _randomRentals = _randomStocks.SelectMany(s => s.Rentals).ToList();
            _rentalRepository = SetupRentalRepository();
            _deviceRepository = SetupDeviceRepository();
            _employeeRepository = SetupEmployeeRepository();
            _stockRepository = SetupStockRepository();
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _rentalService = new RentalService(_rentalRepository,
                _deviceRepository,
                _employeeRepository,
                _stockRepository,
                _unitOfWork);
        }
        //public List<Rental> SetupRentals()
        //{
        //    int counter = new int();
        //    List<Rental> rentals = MockDataInitializer.GenerateRentals();
        //    foreach (Rental rental in rentals)
        //        rental.ID = ++counter;
        //    return rentals;
        //}
        public List<Device> SetupDevices()
        {
            int counter = new int();
            List<Device> devices = MockDataInitializer.GenerateDevices();
            foreach (Device d in devices)
                d.ID = ++counter;
            return devices;
        }
        public List<Employee> SetupEmployees()
        {
            int counter = new int();
            List<Employee> employees = MockDataInitializer.GenerateEmployees();
            foreach (Employee e in employees)
                e.ID = ++counter;
            return employees;
        }
        public List<Stock> SetupStocks()
        {
            int counter = new int();
            List<Stock> stocks = MockDataInitializer.GenerateStocks();
            foreach (Stock s in stocks)
                s.ID = ++counter;
            Stock firstStock = stocks.First();
            firstStock.Rentals.Add(MockDataInitializer.GenerateRentals().First());
            firstStock.Rentals.First().ID = 1;
            firstStock.Rentals.First().Stock = firstStock;
            firstStock.IsAvailable = false;
            return stocks;
        }
        public IEntityBaseRepository<Rental> SetupRentalRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Rental>>();
            // Setup behavior
            repo.Setup(r => r.GetAll())
                .Returns(_randomRentals.AsQueryable());
            repo.Setup(r => r.GetSingle(It.IsAny<int>()))
                .Returns(new Func<int, Rental>(
                    id => _randomRentals.Find(r => r.ID == id)
                    ));
            repo.Setup(r => r.Add(It.IsAny<Rental>()))
                .Callback(new Action<Rental>(
                    newRental =>
                    {
                        dynamic maxID = _randomRentals.Last().ID;
                        dynamic nextID = maxID + 1;
                        newRental.ID = nextID;
                        _randomRentals.Add(newRental);
                    })
                    );
            return repo.Object;
        }
        public IEntityBaseRepository<Device> SetupDeviceRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Device>>();
            // Setup behavior
            repo.Setup(d => d.GetSingle(It.IsAny<int>()))
                .Returns(new Func<int, Device>(
                    id => _randomDevices.Find(d => d.ID == id)
                    ));
            return repo.Object;
        }
        public IEntityBaseRepository<Employee> SetupEmployeeRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Employee>>();
            // Setup behavior
            repo.Setup(e => e.GetSingle(It.IsAny<int>()))
                .Returns(new Func<int, Employee>(
                    id => _randomEmployee.Find(e => e.ID == id)
                ));
            return repo.Object;
        }
        public IEntityBaseRepository<Stock> SetupStockRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Stock>>();
            // Setup behavior
            repo.Setup(s => s.GetSingle(It.IsAny<int>()))
                .Returns(new Func<int, Stock>(
                    id => _randomStocks.Find(s => s.ID == id)
                    ));
            return repo.Object;
        }
        #endregion
        #region Test
        [Test]
        public void ServiceShouldGetRentalsByEmployeeID()
        {
            var rentals = _rentalService.GetRentalsByEmployeeID(1);

            Assert.That(rentals, Is.EqualTo(_randomRentals.Where(r => r.EmployeeID == 1)));
        }
        [Test]
        public void ServiceShouldRentRental()
        {
            var stock = _stockRepository.GetSingle(1);
            int maxRentalID = _randomRentals.Last().ID;
            _rentalService.RentRental(1, 2);

            Assert.That(stock.IsAvailable, Is.False);
            Assert.That(maxRentalID + 1, Is.EqualTo(_randomRentals.Last().ID));
        }
        [Test]
        public void ServiceShouldReturnRental()
        {
            var returnRental = _randomRentals.Find(r => r.ID == 1);
            _rentalService.ReturnRental(1);

            Assert.That(returnRental.Status, Is.EqualTo("Returned"));
            Assert.That(returnRental.ReturnedDate, Is.Not.Null);
            Assert.That(returnRental.Stock.IsAvailable, Is.True);
        }
        [Test]
        public void ServiceShouldReturnAllRental()
        {
            var rentals = _rentalService.GetAllRentals();

            Assert.That(rentals, Is.EqualTo(_randomRentals));
        }
        #endregion
    }
}
