using Hrm.Application.DTOs.Attendance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Queries
{
    public class GetAttendanceSummaryByOfficeDepartmentSectionRequest:IRequest<object>
    {
        public AttendanceReportFilterDto AtdReportFilterDto { get; set; }
    }
}
