using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRequest
{
    public class TakenLeaveReportDto
    {
        public int LeaveRequestId { get; set; }
        public int EmpId { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveTypeShortName { get; set; }
        public int TakenLeave { get; set; }
    }
}
