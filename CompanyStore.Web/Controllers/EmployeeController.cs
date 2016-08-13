﻿using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Web.Infrastructure.Core;
using CompanyStore.Web.Models;
using CompanyStore.Web.Infrastructure.Extension;
using CompanyStore.Data.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Linq.Dynamic;
using AutoMapper;
using System.Web;
using System.IO;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public HttpResponseMessage Get(HttpRequestMessage request, FormDataCollection data, string status = "all")
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

                var filterEmployees = _employeeRepository
                    .FindBy(x => (x.IsActive == (status == "active") || status == "all"))
                    .Select(e => new EmployeeViewModel
                    {
                        ID = e.ID,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        IsActive = e.IsActive
                    });

                if (!string.IsNullOrEmpty(searchValue))
                    filterEmployees = filterEmployees
                        .Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower()) ||
                            x.LastName.ToLower().Contains(searchValue.ToLower()));

                toltalFilteredEmployees = filterEmployees.Count();
                // Using dynamic linq
                employeesVM = filterEmployees.OrderBy(sortColumn + " " + sortColumnDir).Skip(skip).Take(pageSize).ToList();

                Pagination<EmployeeViewModel> pagination = new Pagination<EmployeeViewModel>()
                {
                    data = employeesVM,
                    draw = draw != null ? Convert.ToInt32(draw) : 1,
                    recordsFiltered = toltalFilteredEmployees,
                    recordsTotal = employeesVM.Count
                };

                response = request.CreateResponse<Pagination<EmployeeViewModel>>(HttpStatusCode.OK, pagination);

                return response;
            });
        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(HttpRequestMessage request, EmployeeViewModel model)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                else
                {
                    Employee newEmployee = new Employee();
                    newEmployee.MapEmployee(model);
                    newEmployee.IsActive = true;
                    newEmployee.UniqueKey = Guid.NewGuid();
                    _employeeRepository.Add(newEmployee);
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK, newEmployee.ID);
                }

                return response;
            });
        }

        [HttpDelete]
        [Route("delete/{employeeId:int}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int employeeId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                Employee deleteEmployee = new Employee { ID = employeeId };
                _employeeRepository.Delete(deleteEmployee);
                _unitOfWork.Commit();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpPost]
        [Route("update/{employeeId:int}")]
        public HttpResponseMessage Update(HttpRequestMessage request, int employeeId, EmployeeViewModel model)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                else
                {
                    Employee updateEmloyee = _employeeRepository.GetSingle(employeeId);
                    updateEmloyee.MapEmployee(model);
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [HttpGet]
        [Route("{employeeId:int}")]
        public HttpResponseMessage Detail(HttpRequestMessage request, int employeeId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                EmployeeViewModel employeeVM = _employeeRepository
                    .FindBy(e => e.ID == employeeId)
                    .Select(e => new EmployeeViewModel
                    {
                        ID = e.ID,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        IsActive = e.IsActive,
                        CreatedDate = e.CreatedDate
                    }).FirstOrDefault();

                
                response = request.CreateResponse<EmployeeViewModel>(HttpStatusCode.OK, employeeVM);

                return response;
            });
        }

        [HttpGet]
        [Route("{filter?}")]
        public HttpResponseMessage Filter(HttpRequestMessage request, string filter = null)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<Employee> employees = _employeeRepository.GetEmployeeByFilter(filter);

                IEnumerable<EmployeeViewModel> employeesVM = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                response = request.CreateResponse<IEnumerable<EmployeeViewModel>>(HttpStatusCode.OK, employeesVM);

                return response;
            });
        }

        [MimeMultipart]
        [Route("{employeeId:int}/upload/image")]
        public HttpResponseMessage UploadImage(HttpRequestMessage request, int employeeId)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var employee = _employeeRepository.GetSingle(employeeId);
                if (employee == null)
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found.");
                else
                {
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/employees");
                    var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);
                    // Read MIME multipart
                    Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
                    string _localFileName = multipartFormDataStreamProvider
                        .FileData.Select(m => m.LocalFileName).FirstOrDefault();
                    // Create Response
                    FileUploadResult result = new FileUploadResult
                    {
                        LocalFilePath = _localFileName,
                        FileName = Path.GetFileName(_localFileName),
                        FileLength = new FileInfo(_localFileName).Length
                    };
                    employee.Image = result.FileName;
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }

                return response;
            });
        }
    }
}