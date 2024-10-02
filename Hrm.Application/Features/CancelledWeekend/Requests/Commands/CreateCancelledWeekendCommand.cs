using Hrm.Application.DTOs.CancelledWeekend;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CancelledWeekend.Requests.Commands
{
    public class CreateCancelledWeekendCommand: IRequest<BaseCommandResponse>
    {
        public CreateCancelledWeekendDto CancelledWeekend { get; set; }
    }
}
