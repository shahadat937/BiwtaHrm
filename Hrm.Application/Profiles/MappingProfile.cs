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
using Hrm.Application.DTOs.WeekDay;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using hrm.Application.DTOs.Modules;
using Hrm.Application.DTOs.Modules;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.DTOs.ScaleGradeView;
using Hrm.Application.DTOs.Grade_cls_type_Vw;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.DTOs.TrainingName;
using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Office;
using Hrm.Application.DTOs.Competence;
using Hrm.Application.DTOs.Language;
using Hrm.Application.DTOs.BankAccountType;
using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.DTOs.Bank;
using Hrm.Application.DTOs.Institute;

using Hrm.Application.DTOs.Relation;
using Hrm.Application.DTOs.Occupation;
using Hrm.Application.DTOs.HairColor;
using Hrm.Application.DTOs.EyesColor;
using Hrm.Application.DTOs.Pool;
using Hrm.Application.DTOs.SubDepartment;
using Hrm.Application.DTOs.UserRole;
using Hrm.Application.DTOs.AspNetUsers;
using Hrm.Application.DTOs.OfficeAddress;
using Hrm.Application.DTOs.ExamType;
using Hrm.Application.DTOs.Board;
using Hrm.Application.DTOs.Section;
using Hrm.Application.DTOs.PostingOrderInfo;
using Hrm.Application.DTOs.TransferApproveInfo;
using Hrm.Application.DTOs.DepReleaseInfo;
using Hrm.Application.DTOs.EmpTnsferPostingJoin;
using Hrm.Application.DTOs.Year;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.EmpPresentAddress;
using Hrm.Application.DTOs.EmpPermanentAddress;
using Hrm.Application.DTOs.EmpJobDetail;
using Hrm.Application.DTOs.EmpSpouseInfo;
using Hrm.Application.DTOs.EmpChildInfo;
using Hrm.Application.DTOs.EmpEducationInfo;
using Hrm.Application.DTOs.EmpPsiTrainingInfo;
using Hrm.Application.DTOs.EmpBankInfo;
using Hrm.Application.DTOs.EmpForeignTourInfo;
using Hrm.Application.DTOs.EmpLanguageInfo;
using Hrm.Application.DTOs.EmpPhotoSign;
using Hrm.Application.DTOs.SiteVisit;
using Hrm.Application.DTOs.Workday;
using Hrm.Application.DTOs.Holidays;
using Hrm.Application.DTOs.DayType;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.AttendanceType;
using Hrm.Application.DTOs.AttendanceStatus;



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

            CreateMap<DivisionDto, Division>().ReverseMap();
            CreateMap<Division, CreateDivisionDto>().ReverseMap();


            CreateMap<PromotionType, PromotionTypeDto>().ReverseMap();
            CreateMap<PromotionType, CreatePromotionTypeDto>().ReverseMap();

            CreateMap<ThanaDto, Thana>().ReverseMap();
                
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
            
            CreateMap<OfficeBranch, BranchDto>().ReverseMap();
            CreateMap<OfficeBranch, CreateBranchDto>().ReverseMap();

            CreateMap<appraisalFormType, appraisalFormTypeDto>().ReverseMap();
            CreateMap<appraisalFormType,CreateappraisalFormTypeDto>().ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();

            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();

            CreateMap<Designation, DesignationDto>().ReverseMap();
            CreateMap<Designation, CreateDesignationDto>().ReverseMap();

            CreateMap<Designation, DesignationDto>()
            .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));

            CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
            .ForMember(dest => dest.UpperDepartmentName, opt => opt.MapFrom(src => src.UpperDepartment.DepartmentName));



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

            CreateMap<GradeDto, Grade>().ReverseMap();
              
            CreateMap<Grade, CreateGradeDto>().ReverseMap();
            CreateMap<SubGroup, GroupDto>().ReverseMap();
            CreateMap<SubGroup, CreateGroupDto>().ReverseMap();

            CreateMap<Punishment, PunishmentDto>().ReverseMap();
            CreateMap<Punishment, CreatePunishmentDto>().ReverseMap();

            CreateMap<Reward, RewardDto>().ReverseMap();
            CreateMap<Reward, CreateRewardDto>().ReverseMap();

            CreateMap<HolidayType, HolidayTypeDto>().ReverseMap();
            CreateMap<HolidayType, CreateHolidayTypeDto>().ReverseMap();

            CreateMap<WeekDay, WeekDayDto>().ReverseMap();
            CreateMap<WeekDay, CreateWeekDayDto>().ReverseMap();

            CreateMap<OverallEVPromotion, Overall_EV_PromotionDto>().ReverseMap();
            CreateMap<OverallEVPromotion, CreateOverall_EV_PromotionDto>().ReverseMap();

            CreateMap<Scale, ScaleDto>().ReverseMap();
            CreateMap<Scale, CreateScaleDto>().ReverseMap();

            CreateMap<ScaleGradeView, ScaleGradeViewDto>().ReverseMap();
            CreateMap<ScaleGradeView, CreateScaleGradeViewDto>().ReverseMap();
           
            CreateMap<Grade_cls_type_Vw, Grade_cls_type_VwDto>().ReverseMap();
            CreateMap<Grade_cls_type_Vw, CreateGrade_cls_type_VwDto>().ReverseMap();


            CreateMap<SubBranch, SubBranchDto>().ReverseMap();
            CreateMap<SubBranch, CreateSubBranchDto>().ReverseMap();
 

            CreateMap<Relation, RelationDto>().ReverseMap();
            CreateMap<Relation, CreateRelationDto>().ReverseMap();

            CreateMap<Occupation, OccupationDto>().ReverseMap();
            CreateMap<Occupation, CreateOccupationDto>().ReverseMap();
 
            
            CreateMap<TrainingName, TrainingNameDto>().ReverseMap();
            CreateMap<TrainingName, CreateTrainingNameDto>().ReverseMap();
             
            CreateMap<Office, OfficeDto>().ReverseMap();
            CreateMap<Office, CreateOfficeDto>().ReverseMap();
            
            CreateMap<OfficeAddress, OfficeAddressDto>().ReverseMap();
            CreateMap<OfficeAddress, CreateOfficeAddressDto>().ReverseMap();
            
            CreateMap<Competence, CompetenceDto>().ReverseMap();
            CreateMap<Competence, CreateCompetenceDto>().ReverseMap();
            
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<Language, CreateLanguageDto>().ReverseMap();

            CreateMap<BankAccountType, BankAccountTypeDto>().ReverseMap();
            CreateMap<BankAccountType, CreateBankAccountTypeDto>().ReverseMap();

            CreateMap<BankBranch, BankBranchDto>().ReverseMap();
            CreateMap<BankBranch, CreateBankBranchDto>().ReverseMap();

            CreateMap<Bank, BankDto>().ReverseMap();
            CreateMap<Bank, CreateBankDto>().ReverseMap();

            CreateMap<Institute, InstituteDto>().ReverseMap();
            CreateMap<Institute, CreateInstituteDto>().ReverseMap();
 

            CreateMap<Relation, RelationDto>().ReverseMap();
            CreateMap<Relation, CreateRelationDto>().ReverseMap();

            CreateMap<Occupation, OccupationDto>().ReverseMap();
            CreateMap<Occupation, CreateOccupationDto>().ReverseMap();

            CreateMap<HairColor, HairColorDto>().ReverseMap();
            CreateMap<HairColor, CreateHairColorDto>().ReverseMap();

            CreateMap<EyesColor, EyesColorDto>().ReverseMap();
            CreateMap<EyesColor, CreateEyesColorDto>().ReverseMap();

 
            CreateMap<Pool, PoolDto>().ReverseMap();
            CreateMap<Pool, CreatePoolDto>().ReverseMap();

            CreateMap<SubDepartment, SubDepartmentDto>().ReverseMap();
            CreateMap<SubDepartment, CreateSubDepartmentDto>().ReverseMap();
            
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            CreateMap<UserRole, CreateUserRoleDto>().ReverseMap();

            CreateMap<ExamType, ExamTypeDto>().ReverseMap();
            CreateMap<ExamType, CreateExamTypeDto>().ReverseMap();

            CreateMap<Board, BoardDto>().ReverseMap();
            CreateMap<Board, CreateBoardDto>().ReverseMap();

            CreateMap<AspNetUsers, AspNetUserDto>().ReverseMap();
            CreateMap<AspNetUsers, CreateAspNetUserDto>().ReverseMap();
            
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<Section, CreateSectionDto>().ReverseMap();

            CreateMap<PostingOrderInfo, PostingOrderInfoDto>().ReverseMap();
            CreateMap<PostingOrderInfo, CreatePostingOrderInfoDto>().ReverseMap();
            
            CreateMap<TransferApproveInfo, TransferApproveInfoDto>().ReverseMap();
            CreateMap<TransferApproveInfo, CreateTransferApproveInfoDto>().ReverseMap();
            
            CreateMap<DepReleaseInfo, DepReleaseInfoDto>().ReverseMap();
            CreateMap<DepReleaseInfo, CreateDepReleaseInfoDto>().ReverseMap();
            
            CreateMap<EmpTnsferPostingJoin, EmpTnsferPostingJoinDto>().ReverseMap();
            CreateMap<EmpTnsferPostingJoin, CreateEmpTnsferPostingJoinDto>().ReverseMap();

            CreateMap<Year, YearDto>().ReverseMap();
            CreateMap<Year, CreateYearDto>().ReverseMap();

            CreateMap<Employees, EmployeesDto>().ReverseMap();
            CreateMap<Employees, CreateEmployeesDto>().ReverseMap();

            CreateMap<EmpBasicInfo, EmpBasicInfoDto>().ReverseMap();
            CreateMap<EmpBasicInfo, CreateEmpBasicInfoDto>().ReverseMap();

            CreateMap<EmpPersonalInfo, EmpPersonalInfoDto>().ReverseMap();
            CreateMap<EmpPersonalInfo, CreateEmpPersonalInfoDto>().ReverseMap();

            CreateMap<EmpPresentAddress, EmpPresentAddressDto>().ReverseMap();
            CreateMap<EmpPresentAddress, CreateEmpPresentAddressDto>().ReverseMap();

            CreateMap<EmpPermanentAddress, EmpPermanentAddressDto>().ReverseMap();
            CreateMap<EmpPermanentAddress, CreateEmpPermanentAddressDto>().ReverseMap();

            CreateMap<EmpJobDetail, EmpJobDetailDto>().ReverseMap();
            CreateMap<EmpJobDetail, CreateEmpJobDetailDto>().ReverseMap();

            CreateMap<EmpSpouseInfo, EmpSpouseInfoDto>().ReverseMap();
            CreateMap<EmpSpouseInfo, CreateEmpSpouseInfoDto>().ReverseMap();

            CreateMap<EmpChildInfo, EmpChildInfoDto>().ReverseMap();
            CreateMap<EmpChildInfo, CreateEmpChildInfoDto>().ReverseMap();

            CreateMap<EmpEducationInfo, EmpEducationInfoDto>().ReverseMap();
            CreateMap<EmpEducationInfo, CreateEmpEducationInfoDto>().ReverseMap();

            CreateMap<EmpPsiTrainingInfo, EmpPsiTrainingInfoDto>().ReverseMap();
            CreateMap<EmpPsiTrainingInfo, CreateEmpPsiTrainingInfoDto>().ReverseMap();

            CreateMap<EmpBankInfo, EmpBankInfoDto>().ReverseMap();
            CreateMap<EmpBankInfo, CreateEmpBankInfoDto>().ReverseMap();

            CreateMap<EmpForeignTourInfo, EmpForeignTourInfoDto>().ReverseMap();
            CreateMap<EmpForeignTourInfo, CreateEmpForeignTourInfoDto>().ReverseMap();

            CreateMap<EmpLanguageInfo, EmpLanguageInfoDto>().ReverseMap();
            CreateMap<EmpLanguageInfo, CreateEmpLanguageInfoDto>().ReverseMap();

            CreateMap<EmpPhotoSign, EmpPhotoSignDto>().ReverseMap();
            CreateMap<EmpPhotoSign, CreateEmpPhotoSignDto>().ReverseMap();

            // Site Visit Dto Mapping Created By Joy
            CreateMap<SiteVisit, SiteVisitDto>().ReverseMap();
            CreateMap<SiteVisit, CreateSiteVisitDto>().ReverseMap();
            CreateMap<Workday, WorkdayDto>().ReverseMap();
            CreateMap<Workday,  CreateWorkdayDto>().ReverseMap();
            CreateMap<Holidays, HolidayDto>().ReverseMap();
            CreateMap<Holidays, CreateHolidayDto>().ReverseMap();
            CreateMap<DayType, DayTypeDto>().ReverseMap();
            CreateMap<DayType, CreateDayTypeDto>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<Attendance, CreateAttendanceDto>().ReverseMap();
            CreateMap<DayType, DayTypeDto>().ReverseMap();
            CreateMap<DayType, CreateDayTypeDto>().ReverseMap();
            CreateMap<AttendanceType, AttendanceTypeDto>().ReverseMap();
            CreateMap<AttendanceType, CreateAttendanceTypeDto>().ReverseMap();
            CreateMap<AttendanceStatus, AttendanceStatusDto>().ReverseMap();
            CreateMap<AttendanceStatus, CreateAttendanceStatusDto>().ReverseMap();

            CreateMap<SiteVisit, SiteVisitDto>()
            .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.Employees.EmpEngName));

            CreateMap<Workday, WorkdayDto>()
            .ForMember(dest => dest.YearName, opt => opt.MapFrom(src => src.year.YearName))
            .ForMember(dest => dest.WeekDayName, opt => opt.MapFrom(src => src.weekDay.WeekDayName));

            CreateMap<Holidays, HolidayDto>()
            .ForMember(dest => dest.YearName, opt => opt.MapFrom(src => src.Year.YearName))
            .ForMember(dest => dest.HolidayTypeName, opt => opt.MapFrom(src => src.HolidayType.HolidayTypeName));

            CreateMap<Attendance, AttendanceDto>()
            .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.Employees.EmpEngName))
            .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
            .ForMember(dest=>dest.OfficeBranchName, opt=>opt.MapFrom(src=>src.OfficeBranch.BranchName))
            .ForMember(dest => dest.DayTypeName, opt => opt.MapFrom(src => src.DayType.DayTypeName))
            .ForMember(dest => dest.AttendanceTypeName, opt => opt.MapFrom(src=>src.AttendanceType.AttendanceTypeName));

        }
    }
}
