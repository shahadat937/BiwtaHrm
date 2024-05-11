using Hrm.Application.DTOs.WeekDay;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.WeekDay.Requests.Queries
{
    public class GetWeekDayDetailRequest : IRequest<WeekDayDto>
    {
        public int WeekDayId { get; set; }
    }
}
