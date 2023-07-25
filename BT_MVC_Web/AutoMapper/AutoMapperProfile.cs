using AutoMapper;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Models;
using BT_MVC_Web.ViewModels;

namespace BT_MVC_Web.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityGetDto>().ReverseMap();
            CreateMap<Ward, WardGetDto>().ReverseMap();
            CreateMap<District, DistrictGetDto>().ReverseMap();
            CreateMap<District, DistrictCreatDto>().ReverseMap();
            CreateMap<Employee, NewEmployeeDto>().ReverseMap();
            CreateMap<Ethnicity, EthnicityGetDto>().ReverseMap();
            CreateMap<Occupation, OccupationGetDto>().ReverseMap();
            CreateMap<Employee, EmployeeExport>().ReverseMap();
        }
    }
}
