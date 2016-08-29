using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Extension
{
    public static class DeviceExtension
    {
        public static IEnumerable<Device> GetLatestDevices(this IEntityBaseRepository<Device> deviceRepository)
        {
            return deviceRepository.GetAll()
                .OrderBy(d => new { d.CreatedDate, d.ID })
                .Take(6);
        }
    }
}
