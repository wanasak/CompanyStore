using AutoMapper;
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
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Department> _departmentRepository;

        public DepartmentController(IEntityBaseRepository<Department> departmentRepository,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var departments = _departmentRepository.GetAll().ToList();

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

                var result = _departmentRepository.GetDepartmentEmployeeChart();

                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });
        }
    }
}