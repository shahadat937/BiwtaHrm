using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hrm.Application.Features.Attendance.Requests.Queries
{
    public class GetAttendanceSummaryDetailRequest : IRequest<object>
    {
        public int EmpId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
