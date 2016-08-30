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
    }
}
