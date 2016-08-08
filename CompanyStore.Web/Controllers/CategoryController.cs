using AutoMapper;
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
using System.Web.Http;

namespace CompanyStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/category")]
    public class CategoryController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Category> _categoryRepository;

        public CategoryController(
            IEntityBaseRepository<Category> categoryRepository,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                var categories = _categoryRepository.GetAll().ToList();

                IEnumerable<CategoryViewModel> categoriesVM = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

                response = request.CreateResponse<IEnumerable<CategoryViewModel>>(HttpStatusCode.OK, categoriesVM);

                return response;
            });
        }
    }
}