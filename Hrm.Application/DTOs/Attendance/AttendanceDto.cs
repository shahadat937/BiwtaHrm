using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class AttendanceDto:IAttendanceDto
    {
        public int AttendanceId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int AttendanceTypeId { get; set; }
        public int EmpId { get; set; }
        public int? OfficeId { get; set; }
        public int? OfficeBranchId { get; set; }
        public int? ShiftId { get; set; }
        public int? DayTypeId { get; set; }
        public TimeOnly? InTime { get; set; }
        public TimeOnly? OutTime { get; set; }
        public TimeOnly? BreakTime { get; set; }
        public TimeOnly? ResumeTime { get; set; }
        public int? WorkHour { get; set; }
        public int? OverTime { get; set; }
        public int? ShortTime { get; set; }
        public int? AttendanceStatusId { get; set; }
        public int? Done { get; set; }
        public string? Remark { get; set; }
        public int? SiteVisitId { get; set; }

        public string? AttendanceTypeName { get; set; }
        public string? EmpFirstName { get; set; }
        public string? EmpLastName { get; set; }
        public string? OfficeName { get; set; }
        public string? OfficeBranchName { get; set; }
        public string? ShiftName { get; set; }
        public string? DayTypeName { get; set; }
        public string? AttendanceStatusName { get; set; }
    }
}
