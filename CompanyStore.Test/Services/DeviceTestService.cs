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
    public class DeviceTestService
    {
        #region variables
        private List<Device> _randomDevices; 
        private IDeviceService _deviceService;
        private IEntityBaseRepository<Device> _deviceRepository;
        private IUnitOfWork _unitOfWork;
        #endregion
        #region Setup
        [SetUp]
        public void Setup()
        {
            _randomDevices = SetupDevices();
            _deviceRepository = SetupDeviceRepository();
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _deviceService = new DeviceService(_deviceRepository, _unitOfWork);
        }
        public List<Device> SetupDevices()
        {
            int counter = new int();
            List<Device> devices = MockDataInitializer.GenerateDevices().ToList();
            foreach (Device d in devices)
                d.ID = ++counter;
            return devices;
        }
        public IEntityBaseRepository<Device> SetupDeviceRepository()
        {
            // Init repo 
            var repo = new Mock<IEntityBaseRepository<Device>>();
            // Setup behavior
            repo.Setup(d => d.GetAll())
                .Returns(_randomDevices.AsQueryable());
            return repo.Object;
        }
        #endregion
        #region Test
        [Test]
        public void ServiceShouldReturnLatestDevices()
        {
            
        }
        #endregion
    }
}
