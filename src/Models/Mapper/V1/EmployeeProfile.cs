using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebAPIModels.Models.V1;

namespace WebAPIModels.Mapper.V1
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Company,
                           opt => opt.MapFrom(src => src.Company));
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Company,
                           opt=>opt.MapFrom(src=>src.Company));

            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<Employee, EmployeeForCreationDto>();
        }
    }
}
