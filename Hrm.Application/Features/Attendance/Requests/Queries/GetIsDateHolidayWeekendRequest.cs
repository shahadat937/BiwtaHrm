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
        public int Month {  get; set; }
        public int Year { get; set; }
    }
}
