using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Requests.Commands
{
    public class DenyLeaveRequestFinalCommand: IRequest<BaseCommandResponse>
    {
        public int LeaveRequestId { get; set; }
    }
}
