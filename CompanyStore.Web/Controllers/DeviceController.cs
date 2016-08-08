using AutoMapper;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using CompanyStore.Web.Models;
using CompanyStore.Web.Infrastructure.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
                    .OrderByDescending(d => new { d.CreatedDate, d.ID })
                    .Take(6)
                    .ToList();

                IEnumerable<DeviceViewModel> deviceVM = Mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);

                response = request.CreateResponse<IEnumerable<DeviceViewModel>>(HttpStatusCode.OK, deviceVM); 

                return response;
            });
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Detail(HttpRequestMessage request, int id)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var device = _deviceRepository.GetSingle(id);

                DeviceViewModel deviceVM = Mapper.Map<Device, DeviceViewModel>(device);

                response = request.CreateResponse<DeviceViewModel>(HttpStatusCode.OK, deviceVM);

                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, DeviceViewModel model)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                else
                {
                    Device newDevice = new Device();
                    newDevice.MapDevice(model);
                    for (int i = 0; i < model.NumberOfStocks; i++)
                    {
                        Stock stock = new Stock()
                        {
                            IsAvaiable = true,
                            UniqueKey = Guid.NewGuid(),
                            Device = newDevice
                        };
                        newDevice.Stocks.Add(stock);
                    }
                    _deviceRepository.Add(newDevice);
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Upload(HttpRequestMessage request, int deviceId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var device = _deviceRepository.GetSingle(deviceId);
                if (device == null)
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid device.");
                else
                {
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/devices");
                    var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);
                    // Read the MIME multipart 
                    Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
                    string _localFileName = multipartFormDataStreamProvider
                        .FileData.Select(m => m.LocalFileName).FirstOrDefault();
                    // Create response
                    FileUploadResult result = new FileUploadResult
                    {
                        LocalFilePath = _localFileName,
                        FileName = Path.GetFileName(_localFileName),
                        FileLength = new FileInfo(_localFileName).Length
                    };
                    // Update database
                    device.Image = result.FileName;
                    _deviceRepository.Edit(device);
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
    }
}