using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttendanceStatus
{
    public interface IAttendanceStatusDto
    {
        public int AttendanceStatusId { get; set; }
        public string AttendanceStatusName { get; set; }
    }
}
