using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTransferPosting
{
    public interface IEmpTransferPostingDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? ApplicationById { get; set; }
        public int? CurrentOfficeId { get; set; }
        public DateOnly? CurrentDeptJoinDate { get; set; }
        public int? CurrentDepartmentId { get; set; }
        public int? CurrentSectionId { get; set; }
        public int? CurrentDesignationId { get; set; }
        public int? CurrentGradeId { get; set; }
        public int? CurrentScaleId { get; set; }
        public int? CurrentBasicPay { get; set; }
        public string? OfficeOrderNo { get; set; }
        public DateOnly? OfficeOrderDate { get; set; }
        public int? OrderOfficeById { get; set; }
        public int? ReleaseTypeId { get; set; }
        public int? TransferOfficeId { get; set; }
        public int? TransferDepartmentId { get; set; }
        public int? TransferSectionId { get; set; }
        public int? TransferDesignationId { get; set; }
        public int? UpdateGradeId { get; set; }
        public int? UpdateScaleId { get; set; }
        public int? UpdateBasicPay { get; set; }

        public bool? WithPromotion { get; set; }
        public bool? IsTransferApprove { get; set; }
        public int? TransferApproveById { get; set; }
        public DateOnly? TransferApproveDate { get; set; }
        public string? ApproveRemark { get; set; }
        public bool? TransferApproveStatus { get; set; }

        public bool? IsDepartmentApprove { get; set; }
        public int? DeptReleaseTypeId { get; set; }
        public int? DeptReleaseById { get; set; }
        public DateOnly? DeptReleaseDate { get; set; }
        public int? ReferenceNo { get; set; }
        public bool? DeptClearance { get; set; }
        public string? DeptRemark { get; set; }
        public bool? DeptApproveStatus { get; set; }

        public bool? IsJoining { get; set; }
        public int? JoiningReportingById { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public string? JoiningRemark { get; set; }
        public bool? JoiningStatus { get; set; }

        public bool? ApplicationStatus { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
