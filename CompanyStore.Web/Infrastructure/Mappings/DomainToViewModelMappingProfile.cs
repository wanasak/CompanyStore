using AutoMapper;
using CompanyStore.Entity;
using CompanyStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Device, DeviceViewModel>()
                .ForMember(vm => vm.IsAvailable,
                    map => map.MapFrom(d => d.Stocks.Any(s => s.IsAvailable)))
                .ForMember(vm => vm.Category, map => map.MapFrom(d => d.Category.Name))
                .ForMember(vm => vm.NumberOfStocks, map => map.MapFrom(d => d.Stocks.Count))
                .ForMember(vm => vm.NumberOfStocksAvaiable, map => map.MapFrom(d => d.Stocks.Where(s => s.IsAvailable).Count()));
            Mapper.CreateMap<Category, CategoryViewModel>()
                .ForMember(vm => vm.NumberOfDevices, 
                map => map.MapFrom(c => c.Devices.Count()));
            Mapper.CreateMap<Stock, StockViewModel>();
            Mapper.CreateMap<Employee, EmployeeViewModel>()
                .ForMember(vm => vm.DepartmentName, map => map.MapFrom(e => e.Department.Name));
            Mapper.CreateMap<Department, DepartmentViewModel>()
                .ForMember(vm => vm.NumberOfEmployee,
                map => map.MapFrom(d => d.Employees.Count()));
        }
    }
}
