using Hrm.Application.DTOs.LeaveRequest;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetLeaveRequestByFilterRequest: IRequest<object>
    {
        public LeaveRequestFilterDto filterDto {  get; set; }
    }
}
