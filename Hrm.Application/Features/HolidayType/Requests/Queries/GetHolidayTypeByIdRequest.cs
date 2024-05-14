using Hrm.Application.DTOs.HolidayType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HolidayType.Requests.Queries
{
    public class GetHolidayTypeByIdRequest : IRequest<HolidayTypeDto>
    {
        public int HolidayTypeId { get; set; }
    }
}
