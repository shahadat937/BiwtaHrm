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
    public class UpdateHolidaysByGroupIdCommand: IRequest<BaseCommandResponse>
    {
        public int GroupId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public CreateHolidayDto HolidayDto { get; set; }
    }
}
