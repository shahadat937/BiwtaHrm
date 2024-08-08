using Hrm.Application.DTOs.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetLeaveAmountRequest:IRequest<object>
    {
        public LeaveAmountRequestDto leaveAmountRequestDto { get; set; }
    }
}
