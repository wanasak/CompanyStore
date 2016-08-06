using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Service
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User user { get; set; }
        public bool Isvalid()
        {
            return Principal != null;
        }
    }
}
