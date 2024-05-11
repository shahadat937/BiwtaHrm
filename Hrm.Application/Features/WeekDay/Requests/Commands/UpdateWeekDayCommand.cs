using Hrm.Application.DTOs.WeekDay;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Weekend.Requests.Commands
{
    public class UpdateWeekDayCommand : IRequest<Unit>
    {
        public WeekDayDto WeekendDto { get; set; }
    }
}
