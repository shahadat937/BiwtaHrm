using Hrm.Application.DTOs.Holidays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Requests.Queries
{
    public class GetHolidaysByIdRequest: IRequest<HolidayDto>
    {
        public int HolidayId { get; set; }
    }
}
