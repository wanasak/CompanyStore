using CompanyStore.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CompanyStore.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // Config Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);

            // Config Automapper
            AutoMapperConfiguration.Configure();
        }
    }
}
