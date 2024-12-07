using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hrm.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetTakenLeaveReportRequest : IRequest<object>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> EmpId { get; set; }
    }
}
