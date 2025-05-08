using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Commands
{
    public class UpdateAttendanceQueryRequest : IRequest<BaseCommandResponse>
    {
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
    }
}
