using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpBasicInfo : BaseDomainEntity
    {
        public int Id { get; set; }
        public string? IdCardNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FirstNameBangla { get; set; }
        public string? LastNameBangla { get; set; }
        public string? AspNetUserId { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? NID { get; set; }
        public int? SmartNID { get; set; }
        public DateOnly? ProbabilityRetirementDate { get; set; }
        public DateOnly? PRLDate { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpGovNo { get; set; }
        public string? PersonalFileNo { get; set; }
        public int? EmployeeTypeId { get; set; }
        public bool? UserStatus { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public virtual EmployeeType? EmployeeType { get; set; }
        public virtual ICollection<AspNetUsers>? AspNetUsers { get; set; }
        public virtual ICollection<EmpPersonalInfo>? EmpPersonalInfo { get; set; }
        public virtual ICollection<EmpPresentAddress>? EmpPresentAddress { get; set; }
        public virtual ICollection<EmpPermanentAddress>? EmpPermanentAddress { get; set; }
        public virtual ICollection<EmpJobDetail>? EmpJobDetail { get; set; }
        public virtual ICollection<EmpSpouseInfo>? EmpSpouseInfo { get; set; }
        public virtual ICollection<EmpChildInfo>? EmpChildInfo { get; set; }
        public virtual ICollection<EmpEducationInfo>? EmpEducationInfo { get; set; }
        public virtual ICollection<EmpPsiTrainingInfo>? EmpPsiTrainingInfo { get; set; }
        public virtual ICollection<EmpBankInfo>? EmpBankInfo { get; set; }
        public virtual ICollection<EmpLanguageInfo>? EmpLanguageInfo { get; set; }
        public virtual ICollection<EmpForeignTourInfo>? EmpForeignTourInfo { get; set; }
        public ICollection<Attendance> Attendances { get; } = new List<Attendance>();
        public ICollection<SiteVisit> SiteVisits { get; set; }
        public virtual ICollection<EmpTransferPosting>? EmpTransferPosting { get; set; }
        public virtual ICollection<EmpTransferPosting>? ApplicationTransfer { get; set; }
        public virtual ICollection<EmpTransferPosting>? OrderOfficeTransfer { get; set; }
        public virtual ICollection<EmpTransferPosting>? TransferApproveTransfer { get; set; }
        public virtual ICollection<EmpTransferPosting>? DeptReleaseTransfer { get; set; }
        public virtual ICollection<EmpTransferPosting>? JoiningReportingTransfer { get; set; }

        public virtual ICollection<EmpPromotionIncrement>? EmpPromotionIncrement { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? ApplicationByPromotionIncrement { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? OrderByPromotionIncrement { get; set; }
        public virtual ICollection<EmpPromotionIncrement>? ApproveByPromotionIncrement { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests {get; } = new List<LeaveRequest>();
        public virtual ICollection<FormRecord> FormRecords { get; } = new List<FormRecord>();
        public virtual ICollection<EmpShiftAssign>? EmpShiftAssign { get; set; }
    }
}
