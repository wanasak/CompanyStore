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
            List<Device> devices = MockDataInitializer.GenerateDevices();
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
            repo.Setup(d => d.GetSingle(It.IsAny<int>()))
                .Returns(new Func<int, Device>(
                    id => _randomDevices.Find(d => d.ID.Equals(id))
                ));
            repo.Setup(d => d.Add(It.IsAny<Device>()))
                .Callback(new Action<Device>(
                    newDevice =>
                    {
                        dynamic maxID = _randomDevices.Last().ID;
                        dynamic nextID = maxID + 1;
                        newDevice.ID = nextID;
                        newDevice.CreatedDate = DateTime.Now;
                        _randomDevices.Add(newDevice);
                    })
                );
            repo.Setup(d => d.Edit(It.IsAny<Device>()))
                .Callback(new Action<Device>(
                    updateDevice =>
                    {
                        var originalDevice = _randomDevices.Find(d => d.ID == updateDevice.ID);
                        originalDevice = updateDevice;
                    })
                );
            repo.Setup(d => d.Delete(It.IsAny<Device>()))
                .Callback(new Action<Device>(
                    deleteDevice =>
                    {
                        var removeDevice = _randomDevices.Find(d => d.ID == deleteDevice.ID);
                        if (removeDevice != null)
                            _randomDevices.Remove(removeDevice);
                    })
                );
            return repo.Object;
        }
        #endregion
        #region Test
        [Test]
        public void ServiceShouldReturnLatestDevices()
        {
            var latestDevices = _deviceService.GetLatestDevices(6);

            Assert.That(latestDevices, 
                Is.EqualTo(_randomDevices.OrderByDescending(d => d.CreatedDate).ThenByDescending(d => d.ID).Take(6)));
        }
        [Test]
        public void ServiceShouldReturnRightDevice()
        {
            var device = _deviceService.GetDevice(1);

            Assert.That(device, Is.EqualTo(_randomDevices.Find(d => d.ID.Equals(1))));
        }
        [Test]
        public void ServiceShouldAddNewDevice()
        {
            var newDevice = new Device()
            {
                Name = "New Macbook 2016",
                Description = "Description...",
                CategoryID = 2,
                Price = 55000,
                Image = "macbook2016.png"
            };
            int deviceIDBeforeAdd = _randomDevices.Last().ID;
            _deviceService.CreateDevice(newDevice);

            Assert.That(newDevice, Is.EqualTo(_randomDevices.Last()));
            Assert.That(deviceIDBeforeAdd + 1, Is.EqualTo(_randomDevices.Last().ID));
        }
        [Test]
        public void ServiceShouldUpdateDevice()
        {
            var firstDevice = _randomDevices.First();
            firstDevice.Name = "Changed Name";
            _deviceService.UpdateDevice(firstDevice);

            Assert.That(firstDevice.Name, Is.EqualTo("Changed Name"));
            Assert.That(firstDevice.ID, Is.EqualTo(1));
        }
        [Test]
        public void ServiceShouldDeleteDevice()
        {
            int deviceIDBefore = _randomDevices.Max(d => d.ID);
            var lastDevice = _randomDevices.Last();
            _deviceService.DeleteDevice(lastDevice.ID);

            Assert.That(deviceIDBefore, Is.GreaterThan(_randomDevices.Max(d => d.ID)));
        }
        [Test]
        public void ServiceShouldReturnDevicesByFilter()
        {
            int totalDevices = new int();
            int pageSize = 6;
            var devices = _deviceService.GetDevices(0, pageSize, out totalDevices, "acer", null);

            Assert.That(devices, Is.EqualTo(_randomDevices
                .Where(d => d.Name.ToLower().Contains("acer"))
                .OrderByDescending(d => d.CreatedDate)
                .ThenByDescending(d => d.ID)
                .Skip(0)
                .Take(pageSize)));
            Assert.That(totalDevices, Is.EqualTo(_randomDevices.Where(d => d.Name.ToLower().Contains("acer")).Count()));
        }
        [Test]
        public void ServiceShouldReturnDevicesByCategory()
        {
            int totalDevices = new int();
            int pageSize = 6;
            var devices = _deviceService.GetDevices(0, pageSize, out totalDevices, null, "5");

            Assert.That(devices, Is.EqualTo(_randomDevices
                .Where(d => d.CategoryID == 5)
                .OrderByDescending(d => d.CreatedDate)
                .ThenByDescending(d => d.ID)
                .Skip(0)
                .Take(pageSize)));
            Assert.That(totalDevices, Is.EqualTo(_randomDevices
                .Where(d => d.CategoryID == 5)
                .Count()));
        }
        #endregion
    }
}
