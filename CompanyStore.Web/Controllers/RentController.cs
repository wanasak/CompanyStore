using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using CompanyStore.Web.Models;
using CompanyStore.Data.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/rental")]
    public class RentController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Rental> _rentalRepository;
        private readonly IEntityBaseRepository<Device> _deviceRepository;
        private readonly IEntityBaseRepository<Employee> _employeeRepository;
        private readonly IEntityBaseRepository<Stock> _stockRepository;

        public RentController(
            IEntityBaseRepository<Rental> rentalRepository,
            IEntityBaseRepository<Device> deviceRepository,
            IEntityBaseRepository<Employee> employeeRepository,
            IEntityBaseRepository<Stock> stockRepository,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _rentalRepository = rentalRepository;
            _deviceRepository = deviceRepository;
            _employeeRepository = employeeRepository;
            _stockRepository = stockRepository;
        }

        [HttpPost]
        [Route("rent/{employeeId:int}/{stockId:int}")]
        public HttpResponseMessage Rent(HttpRequestMessage request, int employeeId, int stockId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var employee = _employeeRepository.GetSingle(employeeId);
                var stock = _stockRepository.GetSingle(stockId);

                if (employee == null || stock == null)
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid employee or device.");
                else
                {
                    if (stock.IsAvailable)
                    {
                        Rental rental = new Rental()
                        {
                            EmployeeID = employeeId,
                            StockID = stockId,
                            RentalDate = DateTime.Now,
                            Status = "Borrowed"
                        };
                        _rentalRepository.Add(rental);
                        stock.IsAvailable = false;
                        _unitOfWork.Commit();

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Selected stock is not available");
                }

                return response;
            });
        }

        [HttpPost]
        [Route("return/{rentalId:int}")]
        public HttpResponseMessage Return(HttpRequestMessage request, int rentalId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var rental = _rentalRepository.GetSingle(rentalId);

                if (rental == null)
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid rental.");
                else
                {
                    rental.Status = "Returned";
                    rental.ReturnedDate = DateTime.Now;
                    rental.Stock.IsAvailable = true;
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [HttpGet]
        [Route("{deviceId:int}/rentalHistory")]
        public HttpResponseMessage RentalHistory(HttpRequestMessage request, int deviceId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<RentalHistoryViewModel> rentalHistoryVM = GetMovieRentalHistory(deviceId);

                response = request.CreateResponse<IEnumerable<RentalHistoryViewModel>>(rentalHistoryVM);

                return response;
            });
        }

        [HttpGet]
        [Route("employee/{employeeID:int}")]
        public HttpResponseMessage GetRentalEmployee(HttpRequestMessage request, int employeeID)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                List<RentalHistoryViewModel> rentalHistoriesVM = new List<RentalHistoryViewModel>();
                var rentals = _rentalRepository.GetRentalByEmployeeID(employeeID).OrderByDescending(r => r.ID);

                foreach (var rental in rentals)
                {
                    RentalHistoryViewModel rentalHistoryVM = new RentalHistoryViewModel()
                    {
                        ID = rental.ID,
                        StockID = rental.StockID,
                        RentalDate = rental.RentalDate,
                        ReturnedDate = rental.ReturnedDate.HasValue ? rental.ReturnedDate : null,
                        Status = rental.Status,
                        Device = rental.Stock.Device.Name
                    };
                    rentalHistoriesVM.Add(rentalHistoryVM);
                }

                response = request.CreateResponse<IEnumerable<RentalHistoryViewModel>>(rentalHistoriesVM);

                return response;
            });
        }

        private List<RentalHistoryViewModel> GetMovieRentalHistory(int deviceId)
        {
            List<RentalHistoryViewModel> _rentalHistory = new List<RentalHistoryViewModel>();
            List<Rental> _rentals = new List<Rental>();

            var device = _deviceRepository.GetSingle(deviceId);

            foreach (var stock in device.Stocks)
                _rentals.AddRange(stock.Rentals);

            foreach (var rental in _rentals)
            {
                RentalHistoryViewModel rentalHistoryVM = new RentalHistoryViewModel()
                {
                    ID = rental.ID,
                    StockID = rental.StockID,
                    RentalDate = rental.RentalDate,
                    ReturnedDate = rental.ReturnedDate.HasValue ? rental.ReturnedDate : null,
                    Status = rental.Status,
                    Employee = _employeeRepository.GetEmployeeFullName(rental.EmployeeID)
                };

                _rentalHistory.Add(rentalHistoryVM);
            }

            _rentalHistory.Sort((r1, r2) => r2.RentalDate.CompareTo(r1.RentalDate));

            return _rentalHistory;
        }
    }
}