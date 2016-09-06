using CompanyStore.Data.Infrastructure;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyStore.Web.Models;
using AutoMapper;
using CompanyStore.Service;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/stock")]
    public class StockController : ApiControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(
            IStockService stockService,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [Route("device/{deviceId:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int deviceId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<Stock> stocks = _stockService.GetAvailableStocksByDeviceID(deviceId);

                IEnumerable<StockViewModel> stocksVM = Mapper.Map<IEnumerable<Stock>, IEnumerable<StockViewModel>>(stocks);

                response = request.CreateResponse(HttpStatusCode.OK, stocksVM);

                return response;
            });
        }
    }
}