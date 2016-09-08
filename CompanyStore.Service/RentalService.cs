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
    public interface IRentalService
    {
        IEnumerable<Rental> GetRentalsByEmployeeID(int employeeID);
        void RentRental(int employeeID, int stockID);
        void ReturnRental(int rentalID);
        List<Rental> GetRentalHistoryByDeviceID(int deviceID);
        IEnumerable<Rental> GetAllRentals();
    }

    public class RentalService : IRentalService
    {
        private readonly IEntityBaseRepository<Rental> _rentalRepository;
        private readonly IEntityBaseRepository<Device> _deviceRepository;
        private readonly IEntityBaseRepository<Employee> _employeeRepository;
        private readonly IEntityBaseRepository<Stock> _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RentalService(
            IEntityBaseRepository<Rental> rentalRepository,
            IEntityBaseRepository<Device> deviceRepository,
            IEntityBaseRepository<Employee> employeeRepository,
            IEntityBaseRepository<Stock> stockRepository,
            IUnitOfWork unitOfWork)
        {
            _rentalRepository = rentalRepository;
            _deviceRepository = deviceRepository;
            _employeeRepository = employeeRepository;
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Rental> GetRentalsByEmployeeID(int employeeID)
        {
            var rentals = _rentalRepository.GetRentalByEmployeeID(employeeID);
            return rentals;
        }
        public void RentRental(int employeeID, int stockID)
        {
            var employee = _employeeRepository.GetSingle(employeeID);
            var stock = _stockRepository.GetSingle(stockID);

            if (employee == null || stock == null)
                throw new ApplicationException("Invalid employee or device.");

            if (!stock.IsAvailable)
                throw new ApplicationException("Selected stock is not available.");

            Rental rental = new Rental()
            {
                EmployeeID = employeeID,
                StockID = stockID,
                RentalDate = DateTime.Now,
                Status = "Borrowed"
            };
            _rentalRepository.Add(rental);
            stock.IsAvailable = false;
            _unitOfWork.Commit();
        }
        public void ReturnRental(int rentalID)
        {
            var rental = _rentalRepository.GetSingle(rentalID);

            if (rental == null)
                throw new ApplicationException("Invalid rental.");

            rental.Status = "Returned";
            rental.ReturnedDate = DateTime.Now;
            rental.Stock.IsAvailable = true;
            _unitOfWork.Commit();
        }
        public List<Rental> GetRentalHistoryByDeviceID(int deviceID)
        {
            List<Rental> rentals = new List<Rental>();
            var device = _deviceRepository.GetSingle(deviceID);

            foreach (var stock in device.Stocks)
                rentals.AddRange(stock.Rentals);

            rentals.Sort((r1, r2) => r2.RentalDate.CompareTo(r1.RentalDate));

            return rentals;
        }
        public IEnumerable<Rental> GetAllRentals()
        {
            var rentals = _rentalRepository.GetAll();
            return rentals;
        }
    }
}
