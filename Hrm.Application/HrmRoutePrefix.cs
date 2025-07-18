﻿
using Microsoft.VisualBasic;
using static Hrm.Shared.Constant.Constants;

namespace Hrm.Application
{
    public static class HrmRoutePrefix
    {
        private const string HRMRoutePrefixBase = ApiRoutePrefix.RoutePrefixBase + "hrm/";

        public const string Account = HRMRoutePrefixBase + "account";
        public const string Organogram = HRMRoutePrefixBase + "organogram";

        public const string AspNetUsers = HRMRoutePrefixBase + "users";

        public const string BloodGroup = HRMRoutePrefixBase + "blood-group";
        public const string MaritalStatus = HRMRoutePrefixBase + "marital-status";
        public const string EmployeeType = HRMRoutePrefixBase + "employee-type";
        public const string Gender = HRMRoutePrefixBase + "gender";
        public const string Religion = HRMRoutePrefixBase + "religion";
        public const string TrainingType = HRMRoutePrefixBase + "training-type";
        public const string ChildStatus = HRMRoutePrefixBase + "childStatus";
        public const string Division = HRMRoutePrefixBase + "division";

        public const string Promotion_Type = HRMRoutePrefixBase + "promotion_type";

        public const string Thana = HRMRoutePrefixBase + "thana";
        public const string Upazila = HRMRoutePrefixBase + "upazila";
        public const string Union = HRMRoutePrefixBase + "union";
        public const string District = HRMRoutePrefixBase + "district";
        public const string Result = HRMRoutePrefixBase + "Result";
        public const string OfficeBranch = HRMRoutePrefixBase + "officeBranch";
        public const string SubBranch = HRMRoutePrefixBase + "SubBranch";

        public const string Ward = HRMRoutePrefixBase + "ward";
        public const string appraisalFormType = HRMRoutePrefixBase + "appraisalFormType";
        public const string Department = HRMRoutePrefixBase + "department";
        public const string Country = HRMRoutePrefixBase + "country";
        public const string Designation = HRMRoutePrefixBase + "designation";
        public const string Shift = HRMRoutePrefixBase + "Shift";
        public const string Leave = HRMRoutePrefixBase + "Leave";
        public const string Subject = HRMRoutePrefixBase + "Subject";
        public const string GradeType = HRMRoutePrefixBase + "grade-type";
        public const string GradeClass = HRMRoutePrefixBase + "grade-class";
        public const string Grade = HRMRoutePrefixBase + "grade";
        public const string SubGroup = HRMRoutePrefixBase + "subGroup";
        public const string Punishment = HRMRoutePrefixBase + "punishment";
        public const string Reward = HRMRoutePrefixBase + "reward";
        public const string HolidayType = HRMRoutePrefixBase + "holidayType";
        public const string WeekDay = HRMRoutePrefixBase + "weekDay";
        public const string overallEVPromotion = HRMRoutePrefixBase + "overallEVPromotion";
        public const string Relation = HRMRoutePrefixBase + "Relation";
        public const string Scale = HRMRoutePrefixBase + "scale"; 
        public const string TaskName = HRMRoutePrefixBase + "task-name"; 
        public const string Module = HRMRoutePrefixBase + "modules";
        public const string Feature = HRMRoutePrefixBase + "features";
        public const string RoleFeatures = HRMRoutePrefixBase + "roleFeatures";
        public const string ScaleGradeView = HRMRoutePrefixBase + "scaleGradeView";
        public const string PromotionType = HRMRoutePrefixBase + "promotionType";
        public const string Grade_cls_type_Vw = HRMRoutePrefixBase + "grade_cls_type_Vw";
        public const string TrainingName = HRMRoutePrefixBase + "TrainingName";
        public const string Office = HRMRoutePrefixBase + "Office";
        public const string Competence = HRMRoutePrefixBase + "Competence";
        public const string Language = HRMRoutePrefixBase + "Language";
        public const string BankAccountType = HRMRoutePrefixBase + "BankAccountType";
        public const string Bank = HRMRoutePrefixBase + "Bank";
        public const string Institute = HRMRoutePrefixBase + "Institute";
        public const string BankBranch = HRMRoutePrefixBase + "BankBranch";
        public const string Occupation = HRMRoutePrefixBase + "Occupation";
        public const string OfficeAddress = HRMRoutePrefixBase + "OfficeAddress";
        public const string HairColor = HRMRoutePrefixBase + "hairColor";
        public const string EyesColor = HRMRoutePrefixBase + "eyesColor";
        public const string Pool = HRMRoutePrefixBase + "pool";
        public const string SubDepartment = HRMRoutePrefixBase + "subDepartment";
        public const string UserRole = HRMRoutePrefixBase + "userRole";
        public const string AspNetRoles = HRMRoutePrefixBase + "aspNetRoles";
        public const string ExamType = HRMRoutePrefixBase + "examType";
        public const string Board = HRMRoutePrefixBase + "board";
        public const string Section = HRMRoutePrefixBase + "section";
        public const string PostingOrderInfo = HRMRoutePrefixBase + "postingOrderInfo";
        public const string TransferApproveInfo = HRMRoutePrefixBase + "transferApproveInfo";
        public const string DepReleaseInfo = HRMRoutePrefixBase + "depReleaseInfo";
        public const string EmpTnsferPostingJoin = HRMRoutePrefixBase + "EmpTnsferPostingJoin";
        public const string Year = HRMRoutePrefixBase + "year";
        public const string Employee = HRMRoutePrefixBase + "employee";
        public const string ReleaseType = HRMRoutePrefixBase + "releaseType";

        public const string EmpBasicInfo = HRMRoutePrefixBase + "empBasicInfo";
        public const string EmpPersonalInfo = HRMRoutePrefixBase + "empPersonalInfo";
        public const string EmpPresentAddress = HRMRoutePrefixBase + "empPresentAddress";
        public const string EmpPermanentAddress = HRMRoutePrefixBase + "empPermanentAddress";
        public const string EmpJobDetail = HRMRoutePrefixBase + "empJobDetail";
        public const string EmpSpouseInfo = HRMRoutePrefixBase + "empSpouseInfo";
        public const string EmpChildInfo = HRMRoutePrefixBase + "empChildInfo";
        public const string EmpEducationInfo = HRMRoutePrefixBase + "empEducationInfo";
        public const string EmpPsiTrainingInfo = HRMRoutePrefixBase + "empPsiTrainingInfo";
        public const string EmpBankInfo = HRMRoutePrefixBase + "empBankInfo";
        public const string EmpForeigTourInfo = HRMRoutePrefixBase + "empForeignTourInfo";
        public const string EmpLanguageInfo = HRMRoutePrefixBase + "empLanguageInfo";
        public const string EmpPhotoSign = HRMRoutePrefixBase + "empPhotoSign";
        public const string EmpNomineeInfo = HRMRoutePrefixBase + "empNomineeInfo";

        public const string EmpTransferPosting = HRMRoutePrefixBase + "empTransferPosting";
        public const string EmpPromotionIncrement = HRMRoutePrefixBase + "empPromotionIncrement";

        public const string SiteVisit = HRMRoutePrefixBase + "siteVisit";
        public const string Workday = HRMRoutePrefixBase + "workday";
        public const string Holidays = HRMRoutePrefixBase + "holidays";
        public const string DayType = HRMRoutePrefixBase + "dayType";
        public const string AttendanceType = HRMRoutePrefixBase + "attendanceType";
        public const string AttendanceStatus = HRMRoutePrefixBase + "attendanceStatus";
        public const string Attendance = HRMRoutePrefixBase + "attendance";
        public const string LeaveType = HRMRoutePrefixBase + "leaveType";
        public const string LeaveRules = HRMRoutePrefixBase + "leaveRules";
        public const string LeaveRequest = HRMRoutePrefixBase + "leaveRequest";
        public const string Form = HRMRoutePrefixBase + "form";
        public const string FormFieldType = HRMRoutePrefixBase + "formFieldType";
        public const string FormField = HRMRoutePrefixBase + "formField";
        public const string SelectableOption = HRMRoutePrefixBase + "selectableOption";
        public const string FormSchema = HRMRoutePrefixBase + "formSchema";
        public const string FormRecord = HRMRoutePrefixBase + "formRecord";
        public const string FieldRecord = HRMRoutePrefixBase + "fieldRecord";

        public const string EmpShiftAssign = HRMRoutePrefixBase + "empShiftAssign";
        public const string RewardPunishmentType = HRMRoutePrefixBase + "rewardPunishmentType";
        public const string RewardPunishmentPriority = HRMRoutePrefixBase + "rewardPunishmentPriority";

        public const string Widgets = HRMRoutePrefixBase + "widgets";
        public const string CancelledWeekend = HRMRoutePrefixBase + "cancelledWeekend";

        public const string EmpRewardPunishment = HRMRoutePrefixBase + "empRewardPunishment";
        public const string EmpWorkHistory = HRMRoutePrefixBase + "empWorkHistory";
        public const string ResponsibilityType = HRMRoutePrefixBase + "responsibilityType";
        public const string EmpOtherResponsibility = HRMRoutePrefixBase + "empOtherResponsibility";
        public const string SiteSetting = HRMRoutePrefixBase + "siteSetting";
        public const string CourseDuration = HRMRoutePrefixBase + "courseDuration";
        public const string EmpTrainingInfo = HRMRoutePrefixBase + "empTrainingInfo";
        public const string DesignationSetup = HRMRoutePrefixBase + "designationSetup";
        public const string JobDetailsSetup = HRMRoutePrefixBase + "jobDetailsSetup";
        public const string NavbarThem = HRMRoutePrefixBase + "navbarThem";
        public const string NavbarSetting = HRMRoutePrefixBase + "navbarSetting";
        public const string RoleDashboard = HRMRoutePrefixBase + "roleDashboard";
        public const string EmpFingerPrint = HRMRoutePrefixBase + "empFingerPrint";
        public const string DeviceController = "iclock";
        public const string DeviceUserController = HRMRoutePrefixBase + "AttendanceDevice";
        public const string Notification = HRMRoutePrefixBase + "notification";
        public const string FormGroup = HRMRoutePrefixBase + "formGroup";
        public const string FormSection = HRMRoutePrefixBase + "formSection";

        public const string Reporting = HRMRoutePrefixBase + "reporting";

        public const string ShiftType = HRMRoutePrefixBase + "shiftType";
        public const string ShiftSetting = HRMRoutePrefixBase + "shiftSetting";
        public const string RetiredReason = HRMRoutePrefixBase + "retiredReason";
        public const string OrderType = HRMRoutePrefixBase + "orderType";
        public const string OfficeOrder = HRMRoutePrefixBase + "officeOrder";
        public const string FinancialYear = HRMRoutePrefixBase + "financialYear";
    }
}
