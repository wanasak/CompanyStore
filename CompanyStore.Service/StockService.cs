using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Data.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Service
{
    public interface IStockService
    {
        IEnumerable<Stock> GetAvailableStocksByDeviceID(int id);
    }
    public class StockService : IStockService
    {
        private readonly IEntityBaseRepository<Stock> _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(
            IEntityBaseRepository<Stock> stockRepository,
            IUnitOfWork unitOfWork)
        {
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Stock> GetAvailableStocksByDeviceID(int id)
        {
            var stocks = _stockRepository.GetAvailableStocksByDeviceID(id);
            return stocks;
        }
    }
}
