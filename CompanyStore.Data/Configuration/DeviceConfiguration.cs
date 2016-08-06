using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Configuration
{
    public class DeviceConfiguration : EntityBaseConfiguration<Device>
    {
        public DeviceConfiguration()
        {
            Property(d => d.Name).IsRequired().HasMaxLength(100);
            Property(d => d.Description).IsOptional().HasMaxLength(200);
            Property(d => d.CategoryID).IsRequired();
            Property(d => d.CreatedDate).IsRequired();
            HasMany(d => d.Stocks).WithRequired().HasForeignKey(s => s.DeviceID);
        }
    }
}
