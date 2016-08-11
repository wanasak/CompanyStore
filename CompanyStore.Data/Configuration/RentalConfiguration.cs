using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Configuration
{
    public class RentalConfiguration : EntityBaseConfiguration<Rental>
    {
        public RentalConfiguration()
        {
            Property(r => r.EmployeeID).IsRequired();
            Property(r => r.StockID).IsRequired();
            Property(r => r.ReturnedDate).IsOptional();
            Property(r => r.Status).IsRequired().HasMaxLength(10);
        }
    }
}
