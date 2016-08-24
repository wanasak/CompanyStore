using CompanyStore.Data.Infrastructure;
using CompanyStore.Service;
using CompanyStore.Service.Abstract;
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
    [RoutePrefix("api/account")]
    public class AccountController : ApiControllerBase
    {
        private readonly IMembershipService _membershipService;

        public AccountController(
            IMembershipService membershipService,
            IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            _membershipService = membershipService;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel model)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    MembershipContext _userContext = _membershipService.ValidateUser(model.Username, model.Password);
                    if (_userContext != null)
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true, image = _userContext.User.Image });
                    else
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                }
                else
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                
                return response;
            });
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel model)
        {
            return CreateHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                else
                {
                    var createUser = new Entity.User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Username = model.Username
                    };
                    Entity.User user = _membershipService.CreateUser(
                        createUser,
                        model.Password,
                        new int[] { 1 });
                    if (user != null)
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    else
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                }

                return response;
            });
        }
    }
}