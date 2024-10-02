using Hrm.Application.DTOs.Holidays;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Requests.Commands
{
    public class CreateHolidaysCommand : IRequest<BaseCommandResponse>
    {
        public CreateHolidayDto HolidayDto { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
    }
}
