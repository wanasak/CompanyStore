using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Moq;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Service;
using Autofac.Integration.WebApi;
using CompanyStore.Data;
using System.Linq;

//[assembly: OwinStartup(typeof(CompanyStore.Test.Hosting.Startup))]

namespace CompanyStore.Test.Hosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.MapHttpAttributeRoutes();

            // Autofac configuration
            var builder = new ContainerBuilder();
            // Unit od Work
            var unitOfWork = new Mock<IUnitOfWork>();
            builder.RegisterInstance(unitOfWork.Object).As<IUnitOfWork>();
            // Repositories
            var departmentRepository = new Mock<IEntityBaseRepository<Department>>();
            departmentRepository.Setup(d => d.GetAll()).Returns(
                MockDataInitializer.GenerateDepartments().AsQueryable()
            );
            builder.RegisterInstance(departmentRepository.Object).As<IEntityBaseRepository<Department>>();
            // Services
            builder.RegisterAssemblyTypes(typeof(DepartmentService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterInstance(new DepartmentService(departmentRepository.Object));

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseWebApi(config);
        }
    }
}
