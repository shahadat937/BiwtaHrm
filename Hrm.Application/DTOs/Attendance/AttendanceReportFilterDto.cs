using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class AttendanceReportFilterDto
    {
        public int? EmpId { get; set; }
        public int? OfficeId {  get; set; }
        public int? DepartmentId {  get; set; }
        public int? DesignationId {  get; set; }
        public int? ShiftId { get; set; }
        public int? SectionId {  get; set; }
        public DateOnly From {  get; set; }
        public DateOnly To {  get; set; }
    }
}
