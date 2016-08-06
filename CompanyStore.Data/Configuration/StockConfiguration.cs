using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Configuration
{
    public class StockConfiguration : EntityBaseConfiguration<Stock>
    {
        public StockConfiguration()
        {
            Property(s => s.DeviceID).IsRequired();
            Property(s => s.UniqueKey).IsRequired();
            Property(s => s.IsAvaiable).IsRequired();
            HasMany(s => s.Rentals).WithRequired().HasForeignKey(r => r.StockID);
        }
    }
}
