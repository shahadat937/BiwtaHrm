using Hrm.Application.DTOs.Leave;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Leave.Requests.Commands
{
    public class UpdateLeaveCommand : IRequest<Unit>
    {
        public LeaveDto LeaveDto { get; set; }
    }
}
