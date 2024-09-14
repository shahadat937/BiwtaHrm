using Hrm.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class AttendanceSummaryDto
    {
        public int EmpId { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public DateOnly From{ get; set; }
        public DateOnly To { get; set; }
       // public int DepartmentId { get; set; }
       // public int OfficeId { get; set; }
       // public string OfficeName { get; set; }
       // public string DepartmentName { get; set; }
        public int? TotalWorkHour { get; set; }
        public int? TotalOverTime { get; set; }
        public int? TotalWorkingDay { get; set; }
        public int TotalPresent {  get; set; }
        public int TotalAbsent { get; set; }
        public int TotalLate { get; set; }
        public int TotalSiteVisit { get; set; }
        public int TotalOnLeave { get; set; }
        
    }
}
