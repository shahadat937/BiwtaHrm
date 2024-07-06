using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Attendance: BaseDomainEntity
    {
        public int AttendanceId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int? AttendanceTypeId { get; set; }
        public int EmpId { get; set; }
        public int? OfficeId { get; set; }
        public int? OfficeBranchId { get; set; }
        public int? ShiftId { get; set; }
        public int? DayTypeId { get; set; }
        public TimeOnly? InTime { get; set; }
        public TimeOnly? OutTime { get; set; }
        public TimeOnly? BreakTime { get; set; }
        public TimeOnly? ResumeTime { get; set; }
        public int? WorkHour {  get; set; }
        public int? OverTime { get; set; }
        public int? ShortTime { get; set; }
        public int? AttendanceStatusId { get; set; }
        public int? Done {  get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public AttendanceType AttendanceType { get; set; }
        public EmpBasicInfo EmpBasicInfo { get; set; }
        public Office Office { get; set; }
        public OfficeBranch OfficeBranch { get; set; }
        public Shift Shift { get; set; }
        public DayType DayType { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
    }
}
