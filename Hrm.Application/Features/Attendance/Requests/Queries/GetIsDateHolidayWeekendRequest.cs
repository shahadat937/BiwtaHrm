using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Queries
{
    public class GetIsDateHolidayWeekendRequest: IRequest<object>
    {
        public DateOnly From {  get; set; }
        public DateOnly To { get; set; }
    }
}
