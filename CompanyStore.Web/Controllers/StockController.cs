using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using CompanyStore.Data.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyStore.Web.Models;
using AutoMapper;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/stock")]
    public class StockController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Stock> _stockRepository;

        public StockController(
            IEntityBaseRepository<Stock> stockRepository,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Route("device/{deviceId:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int deviceId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<Stock> stocks = _stockRepository.GetStockAvailable(deviceId);

                IEnumerable<StockViewModel> stocksVM = Mapper.Map<IEnumerable<Stock>, IEnumerable<StockViewModel>>(stocks);

                response = request.CreateResponse(HttpStatusCode.OK, stocksVM);

                return response;
            });
        }
    }
}