using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTransferPosting
{
    public class EmpTransferPostingDto : IEmpTransferPostingDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? IdCardNo { get; set; }
        public string? EmpName { get; set; }
        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }
        public string? SectionName { get; set; }
        public int? ApplicationById { get; set; }
        public string? ApplicationByIdCardNo { get; set; }
        public string? ApplicationByEmpName { get; set; }
        public string? ApplicationByDepartmentName { get; set; }
        public string? ApplicationByDesignationName { get; set; }
        public string? ApplicationBySectionName { get; set; }
        public int? CurrentOfficeId { get; set; }
        public int? CurrentDepartmentId { get; set; }
        public int? CurrentDesignationId { get; set; }
        public int? CurrentSectionId { get; set; }
        public string? OfficeOrderNo { get; set; }
        public DateOnly? OfficeOrderDate { get; set; }
        public int? OrderOfficeById { get; set; }

        public string? OrderByIdCardNo { get; set; }
        public string? OrderByEmpName { get; set; }
        public string? OrderByDepartmentName { get; set; }
        public string? OrderByDesignationName { get; set; }
        public string? OrderBySectionName { get; set; }
        public int? ReleaseTypeId { get; set; }
        public string? ReleaseTypeName { get; set; }
        public int? TransferOfficeId { get; set; }
        public int? TransferDepartmentId { get; set; }
        public int? TransferDesignationId { get; set; }
        public int? TransferSectionId { get; set; }
        public string? TransferDepartmentName { get; set; }
        public string? TransferDesignationName { get; set; }
        public string? TransferSectionName { get; set; }
        public bool? IsTransferApprove { get; set; }
        public int? TransferApproveById { get; set; }

        public string? ApproveByIdCardNo { get; set; }
        public string? ApproveByEmpName { get; set; }
        public string? ApproveByDepartmentName { get; set; }
        public string? ApproveByDesignationName { get; set; }
        public string? ApproveBySectionName { get; set; }
        public DateOnly? TransferApproveDate { get; set; }
        public string? ApproveRemark { get; set; }
        public bool? TransferApproveStatus { get; set; }
        public bool? IsDepartmentApprove { get; set; }
        public int? DeptReleaseTypeId { get; set; }
        public string? DeptReleaseTypeName { get; set; }
        public int? DeptReleaseById { get; set; }

        public string? DeptReleaseByIdCardNo { get; set; }
        public string? DeptReleaseByEmpName { get; set; }
        public string? DeptReleaseByDepartmentName { get; set; }
        public string? DeptReleaseByDesignationName { get; set; }
        public string? DeptReleaseBySectionName { get; set; }
        public DateOnly? DeptReleaseDate { get; set; }
        public int? ReferenceNo { get; set; }
        public bool? DeptClearance { get; set; }
        public string? DeptRemark { get; set; }
        public bool? DeptApproveStatus { get; set; }
        public bool? IsJoining { get; set; }
        public int? JoiningReportingById { get; set; }

        public string? JoiningReportingByIdCardNo { get; set; }
        public string? JoiningReportingByEmpName { get; set; }
        public string? JoiningReportingByDepartmentName { get; set; }
        public string? JoiningReportingByDesignationName { get; set; }
        public string? JoiningReportingBySectionName { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public string? JoiningRemark { get; set; }
        public bool? JoiningStatus { get; set; }
        public bool? ApplicationStatus { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
