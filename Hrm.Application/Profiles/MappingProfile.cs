using AutoMapper;
using Hrm.Application.DTOs.appraisalFormType;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Branch;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.Division;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.DTOs.Result;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.DTOs.Union;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.DTOs.Ward;
using Hrm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.DTOs.Subject;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.DTOs.GradeClass;
using Hrm.Application.DTOs.Grade;
using Hrm.Application.DTOs.Group;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.DTOs.Reward;
using Hrm.Application.DTOs.HolidayType;
using Hrm.Application.DTOs.Weekend;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using hrm.Application.DTOs.Modules;
using Hrm.Application.DTOs.Modules;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.DTOs.ScaleGradeView;

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


            CreateMap<PromotionType, PromotionTypeDto>().ReverseMap();
            CreateMap<PromotionType, CreatePromotionTypeDto>().ReverseMap();

            CreateMap<Thana, ThanaDto>().ReverseMap();
            CreateMap<Thana, CreateThanaDto>().ReverseMap();

            CreateMap<Upazila, UpazilaDto>().ReverseMap();
            CreateMap<Upazila, CreateUpazilaDto>().ReverseMap();

            CreateMap<Union, UnionDto>().ReverseMap();
            CreateMap<Union, CreateUnionDto>().ReverseMap();

            CreateMap<District, DistrictDto>().ReverseMap();
            CreateMap<District, CreateDistrictDto>().ReverseMap();

            CreateMap<Result, ResultDto>().ReverseMap();
            CreateMap<Result, CreateResultDto>().ReverseMap();

            CreateMap<Ward, WardDto>().ReverseMap();
            CreateMap<Ward, CreateWardDto>().ReverseMap();
            
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<Branch, CreateBranchDto>().ReverseMap();

            CreateMap<appraisalFormType, appraisalFormTypeDto>().ReverseMap();
            CreateMap<appraisalFormType,CreateappraisalFormTypeDto>().ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();

            CreateMap<Country, CountryDto>().ForMember(dest => dest.CountrytId, opt => opt.MapFrom(src => src.CountryId)).ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();

            CreateMap<Designation, DesignationDto>().ReverseMap();
            CreateMap<Designation, CreateDesignationDto>().ReverseMap();

            CreateMap<Shift, ShiftDto>().ReverseMap();
            CreateMap<Shift, CreateShiftDto>().ReverseMap();

            CreateMap<Leave, LeaveDto>().ReverseMap();
            CreateMap<Leave, CreateLeaveDto>().ReverseMap();

            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Subject, CreateSubjectDto>().ReverseMap();

            CreateMap<GradeType, GradeTypeDto>().ReverseMap();
            CreateMap<GradeType, CreateGradeTypeDto>().ReverseMap();

            CreateMap<GradeClass, GradeClassDto>().ReverseMap();
            CreateMap<GradeClass, CreateGradeClassDto>().ReverseMap();

            CreateMap<Grade, GradeDto>().ReverseMap();
            CreateMap<Grade, CreateGradeDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Group, CreateGroupDto>().ReverseMap();

            CreateMap<Punishment, PunishmentDto>().ReverseMap();
            CreateMap<Punishment, CreatePunishmentDto>().ReverseMap();

            CreateMap<Reward, RewardDto>().ReverseMap();
            CreateMap<Reward, CreateRewardDto>().ReverseMap();

            CreateMap<HolidayType, HolidayTypeDto>().ReverseMap();
            CreateMap<HolidayType, CreateHolidayTypeDto>().ReverseMap();

            CreateMap<Weekend, WeekendDto>().ReverseMap();
            CreateMap<Weekend, CreateWeekendDto>().ReverseMap();

            CreateMap<Overall_EV_Promotion, Overall_EV_PromotionDto>().ReverseMap();
            CreateMap<Overall_EV_Promotion, CreateOverall_EV_PromotionDto>().ReverseMap();

            CreateMap<Scale, ScaleDto>().ReverseMap();
            CreateMap<Scale, CreateScaleDto>().ReverseMap();

            CreateMap<ScaleGradeView, ScaleGradeViewDto>().ReverseMap();
            CreateMap<ScaleGradeView, CreateScaleGradeViewDto>().ReverseMap();

            #region Modules Mapping    
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleFeatureDto>().ReverseMap();
            CreateMap<Module, CreateModuleDto>().ReverseMap();
            #endregion
        }
    }
}
