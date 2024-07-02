using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttendanceStatus
{
    public class AttendanceStatusDto:IAttendanceStatusDto
    {
        public int AttendanceStatusId { get; set; }
        public string AttendanceStatusName { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
    }
}
