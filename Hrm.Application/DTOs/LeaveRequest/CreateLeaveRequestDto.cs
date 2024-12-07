using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRequest
{
    public class CreateLeaveRequestDto:ILeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int EmpId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? LeavePurpose { get; set; }
        public bool IsForeignLeave { get; set; }
        public int? CountryId { get; set; }
        public string? ForeignLeavePurpose { get; set; }
        public string? AccompanyBy { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public int? ReviewedBy { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ReviewerRemark {  get; set; }
        public string? ApproverRemark {  get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsOldLeave { get; set; }
    }
}
