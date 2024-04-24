
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Leave;

namespace Hrm.Application.Features.Leaves.Requests.Queries
{
    public class GetLeaveDetailRequest : IRequest<LeaveDto>
    {
        public int LeaveId { get; set; }
    }
}
