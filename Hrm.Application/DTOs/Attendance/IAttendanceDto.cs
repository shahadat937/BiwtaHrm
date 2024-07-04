using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public interface IAttendanceDto
    {
        public int AttendanceId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int EmpId { get; set; }
    }
}
