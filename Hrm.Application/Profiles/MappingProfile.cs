using AutoMapper;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.Division;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.DTOs.Union;
using Hrm.Application.DTOs.Upazila;
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

            CreateMap<EmployeeType, EmployeeTypeDto>().ReverseMap();
            CreateMap<EmployeeType, CreateEmployeeTypeDto>().ReverseMap();

            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Gender, CreateGenderDto>().ReverseMap();

            CreateMap<MaritalStatus, MaritalStatusDto>().ReverseMap();
            CreateMap<MaritalStatus, CreateMaritalStatusDto>().ReverseMap();

            CreateMap<Religion, ReligionDto>().ReverseMap();
            CreateMap<Religion, CreateReligionDto>().ReverseMap();

            CreateMap<ChildStatus, ChildStatusDto>().ReverseMap();
            CreateMap<ChildStatus, CreateChildStatusDto>().ReverseMap();

            CreateMap<TrainingType, TrainingTypeDto>().ReverseMap();
            CreateMap<TrainingType, CreateTrainingTypeDto>().ReverseMap();

            CreateMap<Division, DivisionDto>().ReverseMap();
            CreateMap<Division, CreateDivisionDto>().ReverseMap();

            CreateMap<Thana, ThanaDto>().ReverseMap();
            CreateMap<Thana, CreateThanaDto>().ReverseMap();

            CreateMap<Upazila, UpazilaDto>().ReverseMap();
            CreateMap<Upazila, CreateUpazilaDto>().ReverseMap();

            CreateMap<Union, UnionDto>().ReverseMap();
            CreateMap<Union, CreateUnionDto>().ReverseMap();
        }
    }
}
