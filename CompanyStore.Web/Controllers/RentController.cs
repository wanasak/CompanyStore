using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
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
    }
}