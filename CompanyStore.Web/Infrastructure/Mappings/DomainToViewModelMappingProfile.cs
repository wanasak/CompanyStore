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
                .ForMember(vm => vm.IsAvilable,
                map => map.MapFrom(d => d.Stocks.Any(s => s.IsAvaiable)));
            Mapper.CreateMap<Category, CategoryViewModel>()
                .ForMember(vm => vm.NumberOfDevices, 
                map => map.MapFrom(c => c.Devices.Count()));
        }
    }
}
