using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRequest
{
    public class LeaveRequestDto: ILeaveRequestDto
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
        public string? AssociatedFile { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string? LeaveTypeName { get; set; }
        public string? CountryName { get; set; }

    }
}
