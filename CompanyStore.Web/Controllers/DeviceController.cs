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
using CompanyStore.Service;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/device")]
    public class DeviceController : ApiControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(
            IDeviceService deviceService,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _deviceService = deviceService;
        }

        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var devices = _deviceService.GetLatestDevices(6);

                IEnumerable<DeviceViewModel> deviceVM = Mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);

                response = request.CreateResponse<IEnumerable<DeviceViewModel>>(HttpStatusCode.OK, deviceVM); 

                return response;
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{page:int=0}/{pageSize=9}/{filter?}/{category?}")]
        public HttpResponseMessage List(HttpRequestMessage request, int? page, int? pageSize, string filter = null, string category = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                
                IEnumerable<Device> devices = null;
                int totalDevices = 0;

                devices = _deviceService.GetDevices(currentPage, currentPageSize, out totalDevices, filter, category);

                IEnumerable<DeviceViewModel> devicesVM = Mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);

                PaginationSet<DeviceViewModel> pageSet = new PaginationSet<DeviceViewModel>()
                {
                    Page = currentPage,
                    Items = devicesVM,
                    TotalCount = totalDevices,
                    TotalPages = (int)Math.Ceiling((decimal)totalDevices / currentPageSize)
                };

                response = request.CreateResponse<PaginationSet<DeviceViewModel>>(HttpStatusCode.OK, pageSet);

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

                var device = _deviceService.GetDevice(id);

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
                    _deviceService.CreateDevice(newDevice);

                    response = request.CreateResponse(HttpStatusCode.OK, newDevice.ID);
                }

                return response;
            });
        }

        [MimeMultipart]
        [Route("{deviceId:int}/upload/image")]
        public HttpResponseMessage Upload(HttpRequestMessage request, int deviceId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var device = _deviceService.GetDevice(deviceId);
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
                    _deviceService.UpdateDevice(device);

                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }
                return response;
            });
        }

        [HttpDelete]
        [Route("{deviceID:int}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int deviceID)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                _deviceService.DeleteDevice(deviceID);
                response = request.CreateResponse(HttpStatusCode.OK);
                return response;
            });
        }
    }
}