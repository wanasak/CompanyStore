using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace CompanyStore.Web.Infrastructure.Core
{
    public class MimeMultipart : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(
                    new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
            }
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        { }
    }
}
