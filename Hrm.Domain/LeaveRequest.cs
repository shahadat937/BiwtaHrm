using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class LeaveRequest: BaseDomainEntity
    {
        public int LeaveRequestId { get; set; }
        public int EmpId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string? LeavePurpose { get; set; }
        public bool IsForeignLeave { get; set; }
        public int? CountryId { get; set; }
        public string? ForeignLeavePurpose { get; set; }
        public string? AccompanyBy { get; set; }
        public string? AssociatedFile { get; set; }
        public int? Status { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public EmpBasicInfo Employee {get; set;}
        public LeaveType LeaveType {get; set;}
        public Country Country {get; set;}
        
    }
}
