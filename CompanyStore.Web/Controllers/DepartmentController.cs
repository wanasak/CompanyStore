using AutoMapper;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using CompanyStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyStore.Service;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var departments = _departmentService.GetDepartments();

                IEnumerable<DepartmentViewModel> departmentsVM = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

                response = request.CreateResponse<IEnumerable<DepartmentViewModel>>(HttpStatusCode.OK, departmentsVM);

                return response;
            });
        }

        [HttpGet]
        [Route("employee/chart")]
        public HttpResponseMessage DepartmentByEmployee(HttpRequestMessage request)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var result = _departmentService.GetDepartmentEmployeeChart();

                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });
        }
    }
}