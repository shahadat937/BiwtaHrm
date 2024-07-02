using Hrm.Application.DTOs.TaskName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class CreateAttendanceDto: IAttendanceDto
    {
        public int AttendanceId { get; set; }
        public DateOnly AttendanceDate {  get; set; }
        public int AttendanceTypeId { get; set; }
        public int EmpId { get; set; }
        public int OfficeId { get; set; }
        public int OfficeBranchId { get; set; }
        public int DayTypeId { get; set; }
        public TimeOnly? InTime { get; set; }
        public TimeOnly? OutTime { get; set; }
        public TimeOnly? BreakTime { get; set; }
        public TimeOnly? ResumeTime { get; set; }
        public float? WorkHour { get; set; }
        public float? OverTime { get; set; }
        public float? ShortTime { get; set; }
        public bool? LeaveTaken { get; set; }
        public bool? IsLate { get; set; }
        public bool? Done { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
