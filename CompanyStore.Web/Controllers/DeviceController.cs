using AutoMapper;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using CompanyStore.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Adimin")]
    [RoutePrefix("api/device")]
    public class DeviceController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Device> _deviceRepository;

        public DeviceController(
            IEntityBaseRepository<Device> deviceRepository, 
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _deviceRepository = deviceRepository;
        }

        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var devices = _deviceRepository.GetAll()
                    .OrderByDescending(d => d.CreatedDate)
                    .Take(6)
                    .ToList();

                IEnumerable<DeviceViewModel> deviceVM = Mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);

                response = request.CreateResponse<IEnumerable<DeviceViewModel>>(HttpStatusCode.OK, deviceVM); 

                return response;
            });
        }
    }
}