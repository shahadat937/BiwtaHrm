using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class AttendanceFilterDto
    {
        public string? keyword { get; set; }
        public int? EmpId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
        public int? PageSize {  get; set; }
        public int? PageIndex { get; set; }

    }
}
