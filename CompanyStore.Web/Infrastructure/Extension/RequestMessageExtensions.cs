using CompanyStore.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace CompanyStore.Web.Infrastructure.Extension
{
    public static class RequestMessageExtensions
    {
        internal static IMembershipService GetMembershipService(this HttpRequestMessage request)
        {
            return request.GetService<IMembershipService>();
        }
        private static T GetService<T>(this HttpRequestMessage request)
        {
            IDependencyScope dependencyScope = request.GetDependencyScope();
            T service = (T)dependencyScope.GetService(typeof(T));
            return service;
        }
    }
}
