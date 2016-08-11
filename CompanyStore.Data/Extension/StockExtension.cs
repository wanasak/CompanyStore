using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Extension
{
    public static class StockExtension
    {
        public static IEnumerable<Stock> GetStockAvailable(this IEntityBaseRepository<Stock> stockRepository, int deviceId)
        {
            return stockRepository.GetAll().Where(s => s.IsAvailable && s.DeviceID == deviceId);
        }
    }
}
