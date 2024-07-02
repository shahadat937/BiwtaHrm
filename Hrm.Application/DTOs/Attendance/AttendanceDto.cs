using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int AttendanceTypeId { get; set; }
        public string AttendanceTypeName { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int OfficeBranchId { get; set; }
        public string OfficeBranchName { get; set; }
        public int DayTypeId { get; set; }
        public string DayTypeName { get; set; }
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
