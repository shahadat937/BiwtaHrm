using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class AttendanceReportDto
    {
        public int EmpId { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public List<PresentDateDto> PresentDates { get; } = new List<PresentDateDto>();
    }
}
