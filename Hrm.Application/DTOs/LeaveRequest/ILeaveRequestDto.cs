using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRequest
{
    public interface ILeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int EmpId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LeavePurpose { get; set; }
        public bool IsForeignLeave { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }


    }
}
