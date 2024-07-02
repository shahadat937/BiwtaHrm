using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance
{
    public interface IAttendanceDto
    {
        public int AttendanceId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int AttendanceTypeId { get; set; }
        public int EmpId { get; set; }
        public int OfficeId { get; set; }
        public int OfficeBranchId { get; set; }
        public int DayTypeId { get; set; }


    }
}
