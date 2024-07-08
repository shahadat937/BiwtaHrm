using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class GetAttendanceSummaryDto
    {
        public int EmpId { get; set; }
        public DateOnly From {  get; set; }
        public DateOnly To { get; set; }
    }
}
