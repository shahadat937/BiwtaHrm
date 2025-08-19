using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveType
{
    public interface ILeaveTypeDto
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public bool? IsCarryForward { get; set; }
        public bool IsActive { get; set; }
    }
}
