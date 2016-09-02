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
using CompanyStore.Service;
using AutoMapper;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/rental")]
    public class RentController : ApiControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentController(
            IRentalService rentalService,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        [Route("rent/{employeeId:int}/{stockId:int}")]
        public HttpResponseMessage Rent(HttpRequestMessage request, int employeeId, int stockId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                _rentalService.RentRental(employeeId, stockId);

                response = request.CreateResponse(HttpStatusCode.OK);

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

                _rentalService.ReturnRental(rentalId);

                response = request.CreateResponse(HttpStatusCode.OK);

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

                List<Rental> rentalsHistory = _rentalService.GetRentalHistoryByDeviceID(deviceId);

                IEnumerable<RentalHistoryViewModel> rentalsHistoryVM = Mapper.Map<List<Rental>, IEnumerable<RentalHistoryViewModel>>(rentalsHistory);

                response = request.CreateResponse<IEnumerable<RentalHistoryViewModel>>(rentalsHistoryVM);

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
                RentalViewModel rentalVM = new RentalViewModel();
                var rentals = _rentalService.GetRentalsByEmployeeID(employeeID);

                rentalVM.TotalRentalsByDate = rentals
                    .GroupBy(r => r.RentalDate.Date)
                    .Select(g => new TotalRentalByDateViewModel
                    {
                        Date = g.Key,
                        TotalRentals = g.Count()
                    }).ToList();

                rentalVM.RentalHistories = Mapper.Map<IEnumerable<Rental>, IEnumerable<RentalHistoryViewModel>>(rentals);

                response = request.CreateResponse<RentalViewModel>(rentalVM);

                return response;
            });
        }
    }
}