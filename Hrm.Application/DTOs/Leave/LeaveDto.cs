using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Leave
{
    public class LeaveDto: ILeaveDto
    {
        public int LeaveId { get; set; }
        public string? LeaveName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
