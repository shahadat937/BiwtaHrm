using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public class PresentDateDto
    {
        public DateOnly AttendanceDate {  get; set; }
        public string AttendanceStatus { get; set; }
    }
}
