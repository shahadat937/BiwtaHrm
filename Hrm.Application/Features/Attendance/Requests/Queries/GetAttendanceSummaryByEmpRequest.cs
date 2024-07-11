using Hrm.Application.DTOs.Attendance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Queries
{
    public class GetAttendanceSummaryByEmpRequest : IRequest<object>
    {
        public GetAttendanceSummaryDto AtdSummaryDto { get; set; }
    }
}
