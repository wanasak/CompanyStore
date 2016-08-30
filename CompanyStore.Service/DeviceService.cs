using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Service
{
    public interface IDeviceService
    { 
        IEnumerable<Device> GetDevices(int cuurentPage, int pageSize, out int totalDevices, string filter = null);
        IEnumerable<Device> GetLatestDevices(int numberOfDevices);
        Device GetDevice(int id);
        void CreateDevice(Device device);
        void DeleteDevice(int id);
    }

    public class DeviceService : IDeviceService
    {
        private readonly IEntityBaseRepository<Device> _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceService(
            IEntityBaseRepository<Device> deviceService,
            IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceService;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Device> GetDevices(int cuurentPage, int pageSize, out int totalDevices, string filter = null)
        {
            totalDevices = 0;
            var devices = _deviceRepository.GetAll();

            

            //if (!string.IsNullOrEmpty(filter))
            //    devices = devices.

            return devices;
        }
        public IEnumerable<Device> GetLatestDevices(int numberOfDevices)
        {
            var devices = _deviceRepository.GetAll()
                .OrderByDescending(d => new { d.CreatedDate, d.ID })
                .Take(numberOfDevices).ToList();
            return devices;
        }
        public Device GetDevice(int id)
        {
            var device = _deviceRepository.GetSingle(id);
            return device;
        }
        public void CreateDevice(Device device)
        {
            _deviceRepository.Add(device);
            _unitOfWork.Commit();
        }
        public void DeleteDevice(int id)
        {
            Device device = new Device() { ID = id };
            _deviceRepository.Delete(device);
            _unitOfWork.Commit();
        }
    }
}
