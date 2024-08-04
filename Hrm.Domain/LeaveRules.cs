using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class LeaveRules : BaseDomainEntity
    {
        public int RuleId { get; set; }
        public int LeaveTypeId { get; set; }
        public string RuleName { get; set; }
        public int RuleValue { get; set; }
        public string? RuleFreq { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public LeaveType LeaveType { get; set; }
    }
}
