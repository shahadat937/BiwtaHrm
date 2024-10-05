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
using Hrm.Application.DTOs.LeaveRequest;
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
using Hrm.Application.DTOs.EmpNomineeInfo;
using Hrm.Application.DTOs.DayType;
using Hrm.Application.DTOs.AttendanceType;
using Hrm.Application.DTOs.AttendanceStatus;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.ReleaseType;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.DTOs.LeaveType;
using Hrm.Application.DTOs.LeaveRules;
using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.DTOs.AspNetUserRoles;
using Hrm.Application.DTOs.Features;
using Hrm.Application.DTOs.Form;
using Hrm.Application.DTOs.FormFieldType;
using Hrm.Application.DTOs.FormField;
using Hrm.Application.DTOs.SelectableOption;
using Hrm.Application.DTOs.FormSchema;
using Hrm.Application.DTOs.FormRecord;
using Hrm.Application.DTOs.FieldRecord;
using Hrm.Application.DTOs.EmpShiftAssign;
using Hrm.Application.DTOs.RewardPunishmentType;
using Hrm.Application.DTOs.RewardPunishmentPriority;
using Hrm.Application.DTOs.CancelledWeekend;
using Hrm.Application.DTOs.EmpRewardPunishment;
using Hrm.Application.DTOs.EmpWorkHistory;
using Hrm.Application.DTOs.ResponsibilityType;
using Hrm.Application.DTOs.EmpOtherResponsibility;
using Hrm.Application.DTOs.SiteSetting;
using Hrm.Application.DTOs.CourseDuration;
using Hrm.Application.DTOs.EmpTrainingInfo;



namespace Hrm.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodGroup, BloodGroupDto>().ReverseMap();
            CreateMap<BloodGroup, CreateBloodGroupDto>().ReverseMap();

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
            CreateMap<appraisalFormType, CreateappraisalFormTypeDto>().ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();

            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();

            CreateMap<Designation, DesignationDto>().ReverseMap();
            CreateMap<Designation, CreateDesignationDto>().ReverseMap();


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
            CreateMap<SubGroup, GroupDto>()
                .ForMember(dest => dest.ExamTypeName, opt => opt.MapFrom(src => src.ExamType.ExamTypeName));

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

            CreateMap<Scale, ScaleDto>()
                .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade.GradeName));

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

            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<Section, CreateSectionDto>().ReverseMap();
            CreateMap<Section, SectionDto>()
                .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.UpperSectionName, opt => opt.MapFrom(src => src.UpperSection.SectionName));

            CreateMap<Designation, DesignationDto>()
            .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
            .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.SectionName))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.EmpJobDetail.FirstOrDefault().EmpBasicInfo.FirstName + " " + src.EmpJobDetail.FirstOrDefault().EmpBasicInfo.LastName));

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
            CreateMap<AspNetUsers, AspNetUserDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Department.DepartmentName))
                .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Designation.DesignationName))
                .ReverseMap();


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

            CreateMap<ReleaseType, ReleaseTypeDto>().ReverseMap();
            CreateMap<ReleaseType, CreateReleaseTypeDto>().ReverseMap();

            CreateMap<EmpBasicInfo, EmpBasicInfoDto>().ReverseMap();
            CreateMap<EmpBasicInfo, CreateEmpBasicInfoDto>().ReverseMap();

            CreateMap<EmpBasicInfo, EmpBasicInfoDto>()
            .ForMember(dest => dest.EmployeeTypeName, opt => opt.MapFrom(src => src.EmployeeType.EmployeeTypeName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.EmpJobDetail.FirstOrDefault().Department.DepartmentName))
            .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.EmpJobDetail.FirstOrDefault().Designation.DesignationName))
            .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.EmpJobDetail.FirstOrDefault().Section.SectionName))
            .ForMember(dest => dest.EmpPhotoName, opt => opt.MapFrom(src => src.EmpPhotoSign.FirstOrDefault().PhotoUrl))
            .ForMember(dest => dest.EmpGenderName, opt => opt.MapFrom(src => src.EmpPersonalInfo.FirstOrDefault().Gender.GenderName));

            CreateMap<EmpPersonalInfo, EmpPersonalInfoDto>().ReverseMap();
            CreateMap<EmpPersonalInfo, CreateEmpPersonalInfoDto>().ReverseMap();
            CreateMap<EmpPersonalInfo, EmpPersonalInfoDto>()
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.GenderName))
            .ForMember(dest => dest.MaritalStatusName, opt => opt.MapFrom(src => src.MaritalStatus.MaritalStatusName))
            .ForMember(dest => dest.BloodGroupName, opt => opt.MapFrom(src => src.BloodGroup.BloodGroupName))
            .ForMember(dest => dest.ReligionName, opt => opt.MapFrom(src => src.Religion.ReligionName))
            .ForMember(dest => dest.HairColorName, opt => opt.MapFrom(src => src.HairColor.HairColorName))
            .ForMember(dest => dest.EyesColorName, opt => opt.MapFrom(src => src.EyesColor.EyesColorName))
            .ForMember(dest => dest.NationalityName, opt => opt.MapFrom(src => src.Country.CountryName));


            CreateMap<EmpPresentAddress, EmpPresentAddressDto>().ReverseMap();
            CreateMap<EmpPresentAddress, CreateEmpPresentAddressDto>().ReverseMap();
            CreateMap<EmpPresentAddress, EmpPresentAddressDto>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
            .ForMember(dest => dest.DivisionName, opt => opt.MapFrom(src => src.Division.DivisionName))
            .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.DistrictName))
            .ForMember(dest => dest.UpazilaName, opt => opt.MapFrom(src => src.Upazila.UpazilaName))
            .ForMember(dest => dest.ThanaName, opt => opt.MapFrom(src => src.Thana.ThanaName));
            //.ForMember(dest => dest.UnionName, opt => opt.MapFrom(src => src.Union.UnionName))
            //.ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.Ward.WardName));

            CreateMap<EmpPermanentAddress, EmpPermanentAddressDto>().ReverseMap();
            CreateMap<EmpPermanentAddress, CreateEmpPermanentAddressDto>().ReverseMap();
            CreateMap<EmpPermanentAddress, EmpPermanentAddressDto>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
            .ForMember(dest => dest.DivisionName, opt => opt.MapFrom(src => src.Division.DivisionName))
            .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.DistrictName))
            .ForMember(dest => dest.UpazilaName, opt => opt.MapFrom(src => src.Upazila.UpazilaName))
            .ForMember(dest => dest.ThanaName, opt => opt.MapFrom(src => src.Thana.ThanaName));
            //.ForMember(dest => dest.UnionName, opt => opt.MapFrom(src => src.Union.UnionName))
            //.ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.Ward.WardName));

            CreateMap<EmpJobDetail, EmpJobDetailDto>().ReverseMap();
            CreateMap<EmpJobDetail, CreateEmpJobDetailDto>().ReverseMap();
            CreateMap<EmpJobDetail, EmpJobDetailDto>()
            .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
            .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.SectionName))
            .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.Designation.DesignationName))
            .ForMember(dest => dest.PresentGradeName, opt => opt.MapFrom(src => src.PresentGrade.GradeName))
            .ForMember(dest => dest.PresentScaleName, opt => opt.MapFrom(src => src.PresentScale.ScaleName))
            .ForMember(dest => dest.FirstDepartmentName, opt => opt.MapFrom(src => src.FirstDepartment.DepartmentName))
            .ForMember(dest => dest.FirstSectionName, opt => opt.MapFrom(src => src.FirstSection.SectionName))
            .ForMember(dest => dest.FirstDesignationName, opt => opt.MapFrom(src => src.FirstDesignation.DesignationName))
            .ForMember(dest => dest.FirstGradeName, opt => opt.MapFrom(src => src.FirstGrade.GradeName))
            .ForMember(dest => dest.FirstScaleName, opt => opt.MapFrom(src => src.FirstScale.ScaleName));

            CreateMap<EmpSpouseInfo, EmpSpouseInfoDto>().ReverseMap();
            CreateMap<EmpSpouseInfo, CreateEmpSpouseInfoDto>().ReverseMap();
            CreateMap<EmpSpouseInfo, EmpSpouseInfoDto>()
            .ForMember(dest => dest.OccupationName, opt => opt.MapFrom(src => src.Occupation.OccupationName));

            CreateMap<EmpChildInfo, EmpChildInfoDto>().ReverseMap();
            CreateMap<EmpChildInfo, CreateEmpChildInfoDto>().ReverseMap();
            CreateMap<EmpChildInfo, EmpChildInfoDto>()
            .ForMember(dest => dest.OccupationName, opt => opt.MapFrom(src => src.Occupation.OccupationName))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.GenderName))
            .ForMember(dest => dest.MaritalStatusName, opt => opt.MapFrom(src => src.MaritalStatus.MaritalStatusName))
            .ForMember(dest => dest.ChildStatusName, opt => opt.MapFrom(src => src.ChildStatus.ChildStatusName));

            CreateMap<EmpEducationInfo, EmpEducationInfoDto>().ReverseMap();
            CreateMap<EmpEducationInfo, CreateEmpEducationInfoDto>().ReverseMap();
            CreateMap<EmpEducationInfo, EmpEducationInfoDto>()
            .ForMember(dest => dest.ExamTypeName, opt => opt.MapFrom(src => src.ExamType.ExamTypeName))
            .ForMember(dest => dest.BoardName, opt => opt.MapFrom(src => src.Board.BoardName))
            .ForMember(dest => dest.SubGroupName, opt => opt.MapFrom(src => src.SubGroup.GroupName));

            CreateMap<EmpPsiTrainingInfo, EmpPsiTrainingInfoDto>().ReverseMap();
            CreateMap<EmpPsiTrainingInfo, CreateEmpPsiTrainingInfoDto>().ReverseMap();
            CreateMap<EmpPsiTrainingInfo, EmpPsiTrainingInfoDto>()
            .ForMember(dest => dest.TrainingName, opt => opt.MapFrom(src =>
            src.TrainingName.TrainingNames));

            CreateMap<EmpBankInfo, EmpBankInfoDto>().ReverseMap();
            CreateMap<EmpBankInfo, CreateEmpBankInfoDto>().ReverseMap();
            CreateMap<EmpBankInfo, EmpBankInfoDto>()
            .ForMember(dest => dest.AccountTypeName, opt => opt.MapFrom(src => src.AccountType.BankAccountTypeName))
            .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.Bank.BankName));
            //.ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BankBranch.BankBranchName));

            CreateMap<EmpForeignTourInfo, EmpForeignTourInfoDto>().ReverseMap();
            CreateMap<EmpForeignTourInfo, CreateEmpForeignTourInfoDto>().ReverseMap();
            CreateMap<EmpForeignTourInfo, EmpForeignTourInfoDto>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName));

            CreateMap<EmpLanguageInfo, EmpLanguageInfoDto>().ReverseMap();
            CreateMap<EmpLanguageInfo, CreateEmpLanguageInfoDto>().ReverseMap();
            CreateMap<EmpLanguageInfo, EmpLanguageInfoDto>()
            .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.Language.LanguageName))
            .ForMember(dest => dest.CompetenceName, opt => opt.MapFrom(src => src.Competence.CompetenceName));

            CreateMap<EmpPhotoSign, EmpPhotoSignDto>().ReverseMap();
            CreateMap<EmpPhotoSign, CreateEmpPhotoSignDto>().ReverseMap();

            CreateMap<EmpNomineeInfo, EmpNomineeInfoDto>().ReverseMap();
            CreateMap<EmpNomineeInfo, CreateEmpNomineeInfoDto>().ReverseMap();


            CreateMap<EmpTransferPosting, EmpTransferPostingDto>().ReverseMap();
            CreateMap<EmpTransferPosting, CreateEmpTransferPostingDto>().ReverseMap();
            CreateMap<EmpTransferPosting, EmpTransferPostingDto>()
            .ForMember(dest => dest.EmpIdCardNo, opt => opt.MapFrom(src => src.EmpBasicInfo.IdCardNo))
            .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.EmpBasicInfo.FirstName + " " + src.EmpBasicInfo.LastName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.CurrentDepartment.DepartmentName))
            .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.CurrentDesignation.DesignationName))
            .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.CurrentSection.SectionName))
            .ForMember(dest => dest.ApplicationByIdCardNo, opt => opt.MapFrom(src => src.ApplicationBy.IdCardNo))
            .ForMember(dest => dest.ApplicationByEmpName, opt => opt.MapFrom(src => src.ApplicationBy.FirstName + " " + src.ApplicationBy.LastName))
            .ForMember(dest => dest.OrderByIdCardNo, opt => opt.MapFrom(src => src.OrderBy.IdCardNo))
            .ForMember(dest => dest.OrderByEmpName, opt => opt.MapFrom(src => src.OrderBy.FirstName + " " + src.EmpBasicInfo.LastName))
            .ForMember(dest => dest.ReleaseTypeName, opt => opt.MapFrom(src => src.ReleaseType.ReleaseTypeName))
            .ForMember(dest => dest.TransferDepartmentName, opt => opt.MapFrom(src => src.TransferDepartment.DepartmentName))
            .ForMember(dest => dest.TransferDesignationName, opt => opt.MapFrom(src => src.TransferDesignation.DesignationName))
            .ForMember(dest => dest.TransferSectionName, opt => opt.MapFrom(src => src.TransferSection.SectionName))
            .ForMember(dest => dest.ApproveByIdCardNo, opt => opt.MapFrom(src => src.TransferApproveBy.IdCardNo))
            .ForMember(dest => dest.ApproveByEmpName, opt => opt.MapFrom(src => src.TransferApproveBy.FirstName + " " + src.TransferApproveBy.LastName))
            .ForMember(dest => dest.DeptReleaseByIdCardNo, opt => opt.MapFrom(src => src.DeptReleaseBy.IdCardNo))
            .ForMember(dest => dest.DeptReleaseByEmpName, opt => opt.MapFrom(src => src.DeptReleaseBy.FirstName + " " + src.DeptReleaseBy.LastName))
            .ForMember(dest => dest.JoiningReportingByIdCardNo, opt => opt.MapFrom(src => src.JoiningReportingBy.IdCardNo))
            .ForMember(dest => dest.JoiningReportingByEmpName, opt => opt.MapFrom(src => src.JoiningReportingBy.FirstName + " " + src.JoiningReportingBy.LastName))
            .ForMember(dest => dest.DeptReleaseTypeName, opt => opt.MapFrom(src => src.DeptReleaseType.ReleaseTypeName));


            CreateMap<EmpPromotionIncrement, EmpPromotionIncrementDto>().ReverseMap();
            CreateMap<EmpPromotionIncrement, CreateEmpPromotionIncrementDto>().ReverseMap();
            CreateMap<EmpPromotionIncrement, EmpPromotionIncrementDto>()
            .ForMember(dest => dest.EmpIdCardNo, opt => opt.MapFrom(src => src.EmpBasicInfo.IdCardNo))
            .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.EmpBasicInfo.FirstName + " " + src.EmpBasicInfo.LastName))
            .ForMember(dest => dest.CurrentDepartmentName, opt => opt.MapFrom(src => src.CurrentDepartment.DepartmentName))
            .ForMember(dest => dest.CurrentDesignationName, opt => opt.MapFrom(src => src.CurrentDesignation.DesignationName))
            .ForMember(dest => dest.CurrentGradeName, opt => opt.MapFrom(src => src.CurrentGrade.GradeName))
            .ForMember(dest => dest.CurrentScaleName, opt => opt.MapFrom(src => src.CurrentScale.ScaleName))
            .ForMember(dest => dest.ApproveByIdCardNo, opt => opt.MapFrom(src => src.ApproveBy.IdCardNo))
            .ForMember(dest => dest.ApproveByName, opt => opt.MapFrom(src => src.ApproveBy.FirstName + " " + src.ApproveBy.LastName))
            .ForMember(dest => dest.UpdateDesignationName, opt => opt.MapFrom(src => src.UpdateDesignation.DesignationName))
            .ForMember(dest => dest.UpdateGradeName, opt => opt.MapFrom(src => src.UpdateGrade.GradeName))
            .ForMember(dest => dest.UpdateScaleName, opt => opt.MapFrom(src => src.UpdateScale.ScaleName));


            CreateMap<AspNetUserRoles, AspNetUserRolesDto>().ReverseMap();

            CreateMap<Domain.Module, ModuleDto>().ReverseMap();
            CreateMap<Domain.Module, CreateModuleDto>().ReverseMap();

            CreateMap<Domain.Feature, FeatureDto>().ReverseMap();
            CreateMap<Domain.Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Domain.Feature, FeatureDto>()
            .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.Module.Title));


            // Site Visit Dto Mapping Created By Joy
            CreateMap<SiteVisit, SiteVisitDto>().ReverseMap();
            CreateMap<SiteVisit, CreateSiteVisitDto>().ReverseMap();
            CreateMap<Workday, WorkdayDto>().ReverseMap();
            CreateMap<Workday, CreateWorkdayDto>().ReverseMap();
            CreateMap<Holidays, HolidayDto>().ReverseMap();
            CreateMap<Holidays, CreateHolidayDto>().ReverseMap();
            CreateMap<DayType, DayTypeDto>().ReverseMap();
            CreateMap<DayType, CreateDayTypeDto>().ReverseMap();
            CreateMap<DayType, DayTypeDto>().ReverseMap();
            CreateMap<DayType, CreateDayTypeDto>().ReverseMap();
            CreateMap<AttendanceType, AttendanceTypeDto>().ReverseMap();
            CreateMap<AttendanceType, CreateAttendanceTypeDto>().ReverseMap();
            CreateMap<AttendanceStatus, AttendanceStatusDto>().ReverseMap();
            CreateMap<AttendanceStatus, CreateAttendanceStatusDto>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<Attendance, CreateAttendanceDto>().ReverseMap();

            CreateMap<SiteVisit, SiteVisitDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employees.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employees.LastName));

            CreateMap<Workday, WorkdayDto>()
            .ForMember(dest => dest.YearName, opt => opt.MapFrom(src => src.year.YearName))
            .ForMember(dest => dest.WeekDayName, opt => opt.MapFrom(src => src.weekDay.WeekDayName));

            CreateMap<Holidays, HolidayDto>()
            .ForMember(dest => dest.YearName, opt => opt.MapFrom(src => src.Year.YearName))
            .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
            .ForMember(dest => dest.OfficeBranchName, opt => opt.MapFrom(src => src.OfficeBranch.BranchName))
            .ForMember(dest => dest.HolidayTypeName, opt => opt.MapFrom(src => src.HolidayType.HolidayTypeName));

            CreateMap<Attendance, AttendanceDto>()
            .ForMember(dest => dest.AttendanceTypeName, opt => opt.MapFrom(src => src.AttendanceType.AttendanceTypeName))
            .ForMember(dest => dest.EmpFirstName, opt => opt.MapFrom(src => src.EmpBasicInfo.FirstName))
            .ForMember(dest => dest.EmpLastName, opt => opt.MapFrom(src => src.EmpBasicInfo.LastName))
            .ForMember(dest => dest.IdCardNo, opt => opt.MapFrom(src => src.EmpBasicInfo.IdCardNo))
            .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
            .ForMember(dest => dest.OfficeBranchName, opt => opt.MapFrom(src => src.OfficeBranch.BranchName))
            .ForMember(dest => dest.ShiftName, opt => opt.MapFrom(src => src.Shift.ShiftName))
            .ForMember(dest => dest.DayTypeName, opt => opt.MapFrom(src => src.DayType.DayTypeName))
            .ForMember(dest => dest.AttendanceStatusName, opt => opt.MapFrom(src => src.AttendanceStatus.AttendanceStatusName));

            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();
            CreateMap<LeaveRules, LeaveRulesDto>().ReverseMap();
            CreateMap<LeaveRules, CreateLeaveRulesDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();

            CreateMap<LeaveRules, LeaveRulesDto>()
            .ForMember(dest => dest.LeaveTypeName, opt => opt.MapFrom(src => src.LeaveType.LeaveTypeName));

            CreateMap<LeaveRequest, LeaveRequestDto>()
             .ForMember(dest => dest.EmpFirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
             .ForMember(dest => dest.EmpLastName, opt => opt.MapFrom(src => src.Employee.LastName))
             .ForMember(dest => dest.LeaveTypeName, opt => opt.MapFrom(src => src.LeaveType.LeaveTypeName))
             .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
             .ForMember(dest => dest.IdCardNo, opt => opt.MapFrom(src => src.Employee.IdCardNo));


            CreateMap<Form, FormDto>().ReverseMap();
            CreateMap<Form, CreateFormDto>().ReverseMap();
            CreateMap<FormFieldType, FormFieldTypeDto>().ReverseMap();
            CreateMap<FormFieldType, CreateFormFieldTypeDto>().ReverseMap();
            CreateMap<FormField, FormFieldDto>().ReverseMap();
            CreateMap<FormField, CreateFormFieldDto>().ReverseMap();
            CreateMap<SelectableOption, CreateSelectableOptionDto>().ReverseMap();
            CreateMap<SelectableOption, SelectableOptionDto>().ReverseMap();
            CreateMap<FormSchema, FormSchemaDto>().ReverseMap();
            CreateMap<FormSchema, CreateFormSchemaDto>().ReverseMap();
            CreateMap<FormRecord, FormRecordDto>().ReverseMap();
            CreateMap<FormRecord, CreateFormRecordDto>().ReverseMap();
            CreateMap<FieldRecord, FieldRecordDto>().ReverseMap();
            CreateMap<FieldRecord, CreateFieldRecordDto>().ReverseMap();
            CreateMap<SelectableOption, SelectableOptionDto>().ReverseMap();
            CreateMap<SelectableOption, CreateSelectableOptionDto>().ReverseMap();

            CreateMap<FormField, FormFieldDto>()
                .ForMember(dest => dest.FieldTypeName, opt => opt.MapFrom(src => src.FieldType.FieldTypeName))
                .ForMember(dest => dest.HTMLTagName, opt => opt.MapFrom(src => src.FieldType.HTMLTagName))
                .ForMember(dest => dest.HTMLInputType, opt => opt.MapFrom(src => src.FieldType.HTMLInputType));

            CreateMap<SelectableOption, SelectableOptionDto>()
                .ForMember(dest => dest.FieldName, opt => opt.MapFrom(src => src.FormField.FieldName));

            CreateMap<FormSchema, FormSchemaDto>()
                .ForMember(dest => dest.FormName, opt => opt.MapFrom(src => src.Form.FormName))
                .ForMember(dest => dest.FieldName, opt => opt.MapFrom(src => src.FormField.FieldName));

            CreateMap<FormRecord, FormRecordDto>()
                .ForMember(dest => dest.EmpFirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.EmpLastName, opt => opt.MapFrom(src => src.Employee.LastName))
                .ForMember(dest => dest.IdCardNo, opt => opt.MapFrom(src => src.Employee.IdCardNo));

            CreateMap<FieldRecord, FieldRecordDto>()
                .ForMember(dest => dest.FieldName, opt => opt.MapFrom(src => src.FormField.FieldName));

            CreateMap<SelectableOption, SelectableOptionDto>()
                .ForMember(dest => dest.FieldName, opt => opt.MapFrom(src => src.FormField.FieldName));


            CreateMap<EmpShiftAssign, EmpShiftAssignDto>().ReverseMap();
            CreateMap<EmpShiftAssign, CreateEmpShiftAssignDto>().ReverseMap();
            CreateMap<EmpShiftAssign, EmpShiftAssignDto>()
            .ForMember(dest => dest.ShiftName, opt => opt.MapFrom(src => src.Shift.ShiftName))
            .ForMember(dest => dest.PMISNo, opt => opt.MapFrom(src => src.EmpBasicInfo.IdCardNo))
            .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.EmpBasicInfo.FirstName + " " + src.EmpBasicInfo.LastName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Department.DepartmentName))
            .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.EmpBasicInfo.EmpJobDetail.FirstOrDefault().Designation.DesignationName));



            CreateMap<RewardPunishmentType, RewardPunishmentTypeDto>().ReverseMap();
            CreateMap<RewardPunishmentType, CreateRewardPunishmentTypeDto>().ReverseMap();

            CreateMap<RewardPunishmentPriority, RewardPunishmentPriorityDto>().ReverseMap();
            CreateMap<RewardPunishmentPriority, CreateRewardPunishmentPriorityDto>().ReverseMap();
            
            CreateMap<CancelledWeekend, CancelledWeekendDto>().ReverseMap();
            CreateMap<CancelledWeekend, CreateCancelledWeekendDto>().ReverseMap();

            CreateMap<CancelledWeekend, CancelledWeekendDto>()
                .ForMember(dest => dest.CancelledByEmpFirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.CancelledByEmpLastName, opt => opt.MapFrom(src => src.Employee.LastName));


            CreateMap<EmpRewardPunishment, EmpRewardPunishmentDto>().ReverseMap();
            CreateMap<EmpRewardPunishment, CreateEmpRewardPunishmentDto>().ReverseMap();
            CreateMap<EmpRewardPunishment, EmpRewardPunishmentDto>()
                .ForMember(dest => dest.EmpIdCardNo, opt => opt.MapFrom(src => src.EmpBasicInfo.IdCardNo))
                .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.EmpBasicInfo.FirstName + " " + src.EmpBasicInfo.LastName))
                .ForMember(dest => dest.RewardPunishmentTypeName, opt => opt.MapFrom(src => src.RewardPunishmentType.Name))
                .ForMember(dest => dest.RewardPunishmentPriorityName, opt => opt.MapFrom(src => src.RewardPunishmentPriority.Name)).ReverseMap();


            CreateMap<EmpWorkHistory, EmpWorkHistoryDto>().ReverseMap();
            CreateMap<EmpWorkHistory, CreateEmpWorkHistoryDto>().ReverseMap();
            CreateMap<EmpWorkHistory, EmpWorkHistoryDto>()
                .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.SectionName))
                .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.Designation.DesignationName));

            CreateMap<ResponsibilityType, ResponsibilityTypeDto>().ReverseMap();
            CreateMap<ResponsibilityType, CreateResponsibilityTypeDto>().ReverseMap();

            CreateMap<EmpOtherResponsibility, EmpOtherResponsibilityDto>().ReverseMap();
            CreateMap<EmpOtherResponsibility, CreateEmpOtherResponsibilityDto>().ReverseMap();
            CreateMap<EmpOtherResponsibility, EmpOtherResponsibilityDto>()
                .ForMember(dest => dest.ResponsibilityName, opt => opt.MapFrom(src => src.ResponsibilityType.Name))
                .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.SectionName))
                .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.Designation.DesignationName));

            CreateMap<SiteSetting, SiteSettingDto>().ReverseMap();
            //CreateMap<SiteSetting, CreateSiteSettingDto>().ReverseMap();


            CreateMap<CourseDuration, CourseDurationDto>().ReverseMap();
            CreateMap<CourseDuration, CreateCourseDurationDto>().ReverseMap();


            CreateMap<EmpTrainingInfo, EmpTrainingInfoDto>().ReverseMap();
            CreateMap<EmpTrainingInfo, CreateEmpTrainingInfoDto>().ReverseMap(); 
            CreateMap<EmpTrainingInfo, EmpTrainingInfoDto>()
                .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(src => src.TrainingType.TrainingTypeName))
                .ForMember(dest => dest.TrainingName, opt => opt.MapFrom(src => src.TrainingName.TrainingNames))
                .ForMember(dest => dest.InstituteName, opt => opt.MapFrom(src => src.Institute.InstituteName))
                .ForMember(dest => dest.TrainingDuration, opt => opt.MapFrom(src => src.CourseDuration.Duration))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName));

        }
    }
}