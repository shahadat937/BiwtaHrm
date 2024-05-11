using Hrm.Application.DTOs.WeekDay;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Weekend.Requests.Commands
{
    public class CreateWeekDayCommand : IRequest<BaseCommandResponse>
    {
        public CreateWeekDayDto WeekendDto { get; set; }
    }
}
