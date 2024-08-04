using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRules
{
    public interface ILeaveRulesDto
    {
        public int RuleId { get; set; }
        public int LeaveTypeID { get; set; }
        public string RuleName { get; set; }
        public int RuleValue { get; set; }
        public bool IsActive { get; set; }
    }
}
