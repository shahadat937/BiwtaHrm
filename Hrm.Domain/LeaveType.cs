using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class LeaveType: BaseDomainEntity
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string? ShortName { get; set; }
        public bool? ELWorkDayCal {  get; set; }
        public bool IsActive { get; set; }
        public bool? ShowReport { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsCarryForward { get; set; }

        public virtual ICollection<LeaveRules> LeaveRules { get; } = new List<LeaveRules>();
        public virtual ICollection<LeaveRequest> LeaveRequests {get; } = new List<LeaveRequest>();
    }
}
