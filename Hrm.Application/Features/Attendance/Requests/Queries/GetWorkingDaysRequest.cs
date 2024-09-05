using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Queries
{
    public class GetWorkingDaysRequest: IRequest<object>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
