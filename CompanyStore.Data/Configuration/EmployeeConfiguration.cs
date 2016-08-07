using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Configuration
{
    public class EmployeeConfiguration : EntityBaseConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            Property(e => e.Email).IsRequired().HasMaxLength(100);
            Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            Property(e => e.LastName).IsRequired().HasMaxLength(50);
            Property(s => s.UniqueKey).IsRequired();
        }
    }
}
