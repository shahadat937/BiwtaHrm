using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRules
{
    public class LeaveRulesDto: ILeaveRulesDto
    {
        public int RuleId { get; set; }
        public int LeaveTypeID { get; set; }
        public string RuleName { get; set; }
        public string? LeaveTypeName { get; set; }
        public int RuleValue { get; set; }
        public string? RuleFreq { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
