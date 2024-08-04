using Hrm.Application.DTOs.LeaveType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveType.Requests.Commands
{
    public class UpdateLeaveTypeCommand: IRequest<BaseCommandResponse>
    {
        public CreateLeaveTypeDto leaveTypeDto {  get; set; }
    }
}
