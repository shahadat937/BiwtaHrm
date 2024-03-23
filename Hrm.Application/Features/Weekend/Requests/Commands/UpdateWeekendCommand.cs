using Hrm.Application.DTOs.Weekend;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Weekend.Requests.Commands
{
    public class UpdateWeekendCommand : IRequest<Unit>
    {
        public WeekendDto WeekendDto { get; set; }
    }
}
