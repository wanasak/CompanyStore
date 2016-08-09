using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using CompanyStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace CompanyStore.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Employee> _employeeRepository;

        public EmployeeController(
            IEntityBaseRepository<Employee> employeeRepository,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("{status?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, FormDataCollection data, string status = "active")
        {
            // Datatable parameter
            var draw = data.GetValues("draw").FirstOrDefault();
            var start = data.GetValues("start").FirstOrDefault();
            var length = data.GetValues("length").FirstOrDefault();
            var searchValue = data.GetValues("search[value]").FirstOrDefault();
            var sortColumn = data.GetValues("columns[" + data.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
            var sortColumnDir = data.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int toltalFilteredEmployees = 0;

            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                List<EmployeeViewModel> employeesVM = new List<EmployeeViewModel>();

                var filterEmployees = _employeeRepository.GetAll()
                    .Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower()))
                    .Select(e => new EmployeeViewModel
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        IsActive = e.IsActive
                    });

                toltalFilteredEmployees = filterEmployees.Count();

                employeesVM = filterEmployees.Skip(skip).Take(pageSize).ToList();

                Pagination<EmployeeViewModel> pagination = new Pagination<EmployeeViewModel>()
                {
                    data = employeesVM,
                    draw = draw != null ? Convert.ToInt32(draw) : 1,
                    recordFiltered = toltalFilteredEmployees,
                    recordTotal = employeesVM.Count
                };

                response = request.CreateResponse<Pagination<EmployeeViewModel>>(HttpStatusCode.OK, pagination);

                return response;
            });
        }
    }
}