using Hrm.Application.DTOs.HolidayType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HolidayType.Requests.Commands
{
    public class UpdateHolidayTypeCommand : IRequest<Unit>
    {
        public HolidayTypeDto HolidayTypeDto { get; set; }
    }
}
