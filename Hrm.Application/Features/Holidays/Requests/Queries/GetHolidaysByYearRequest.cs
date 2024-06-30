using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Requests.Queries
{
    public class GetHolidaysByYearRequest:IRequest<object>
    {
        public int YearName { get; set; }
    }
}
