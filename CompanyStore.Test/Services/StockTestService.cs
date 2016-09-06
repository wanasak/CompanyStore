using CompanyStore.Data;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Test.Services
{
    [TestFixture]
    public class StockTestService
    {
        #region Variables
        private List<Stock> _randomStocks;
        private IEntityBaseRepository<Stock> _stockRepository;
        private IUnitOfWork _unitOfWork;
        private IStockService _stockService;
        #endregion
        #region Setup
        [SetUp]
        public void Setup()
        {
            _randomStocks = SetupStocks();
            _stockRepository = SetupStockRepository();
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _stockService = new StockService(_stockRepository, _unitOfWork);
        }
        public List<Stock> SetupStocks()
        {
            int counter = new int();
            List<Stock> stocks = MockDataInitializer.GenerateStocks();
            foreach (Stock s in stocks)
                s.ID = ++counter;
            return stocks;
        }
        public IEntityBaseRepository<Stock> SetupStockRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Stock>>();
            // Setup behavior
            repo.Setup(r => r.GetAll())
                .Returns(_randomStocks.AsQueryable());
            return repo.Object;
        }
        #endregion
        #region Test
        [Test]
        public void ServiceShouldReturnAvailableStocksByDeviceID()
        {
            var stocks = _stockService.GetAvailableStocksByDeviceID(1);

            Assert.That(stocks, Is.EqualTo(_randomStocks.Where(s => s.IsAvailable && s.DeviceID == 1)));
        }
        #endregion
    }
}
