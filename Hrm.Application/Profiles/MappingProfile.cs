using AutoMapper;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodGroup, BloodGroupDto>().ReverseMap();
            CreateMap<BloodGroup, CreateBloodGroupDto> ().ReverseMap();

            CreateMap<MaritalStatus, MaritalStatusDto>().ReverseMap();
            CreateMap<MaritalStatus, CreateMaritalStatusDto>().ReverseMap();

        }
    }
}
