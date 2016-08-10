using CompanyStore.Entity;
using CompanyStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Infrastructure.Extension
{
    public static class EntitiesExtensions
    {
        public static void MapDevice(this Device device, DeviceViewModel model)
        {
            device.Name = model.Name;
            device.ID = model.ID;
            device.Description = model.Description;
            device.CreatedDate = model.CreatedDate == DateTime.MinValue ? DateTime.Now : model.CreatedDate;
            device.Image = model.Image;
            device.CategoryID = model.CategoryID;
        }
        public static void MapEmployee(this Employee employee, EmployeeViewModel model)
        {
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.CreatedDate = model.CreatedDate;
            employee.IsActive = model.IsActive;
            //employee.UniqueKey = Guid.NewGuid();
        }
    }
}
