using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.DTOs.User;
using EMS.DAL.Models;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BLL.ProfileMapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
 
            //Base  Domain
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<Allowance, AllowanceDto>().ReverseMap();
            CreateMap<Leave, LeaveDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Appreciation, AppreciationDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();


           CreateMap<ApplicationUser, UserDto>()
          .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
          .ReverseMap()
          .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));



            //View Domain

        }

    }
}
