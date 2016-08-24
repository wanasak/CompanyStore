using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Configuration
{
    public class DepartmentConfiguration : EntityBaseConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            Property(d => d.Name).IsRequired().HasMaxLength(50);
        }
    }
}
