using Hrm.Application.DTOs.Leave;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Leave.Requests.Commands
{
    public class UpdateLeaveCommand : IRequest<BaseCommandResponse>
    {
        public required LeaveDto LeaveDto { get; set; }
    }
}
